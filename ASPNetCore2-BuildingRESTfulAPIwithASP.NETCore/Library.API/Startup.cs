using AspNetCoreRateLimit;
using Library.API.Entities;
using Library.API.Helpers;
using Library.API.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using NLog.Extensions.Logging;
using System.Linq;

namespace Library.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(setupAction => {
                setupAction.ReturnHttpNotAcceptable = true;
                setupAction.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());   //  Accept : application/xml
                                                                                                    //setupAction.InputFormatters.Add(new XmlDataContractSerializerInputFormatter());     //  Content-Type: application/xml

                #region Register XML Input Formatter for new media type
                var xmlDataContractSerializerInputFormatter =
                new XmlDataContractSerializerInputFormatter();
                xmlDataContractSerializerInputFormatter.SupportedMediaTypes
                    .Add("application/vnd.marvin.authorwithdateofdeath.full+xml");
                setupAction.InputFormatters.Add(xmlDataContractSerializerInputFormatter);
                #endregion

                #region Registering Input Formatter Media Links

                var jsonInputFormatter = setupAction.InputFormatters
                .OfType<JsonInputFormatter>().FirstOrDefault();
                if (jsonInputFormatter != null)
                {
                    jsonInputFormatter.SupportedMediaTypes
                    .Add("application/vnd.marvin.author.full+json");
                    jsonInputFormatter.SupportedMediaTypes
                    .Add("application/vnd.marvin.authorwithdateofdeath.full+json");
                }
                #endregion

                #region Registering Custom Content Negotiation for Output Formatters => Accept : application/vnd.marvin.hateoas+json
                var jsonOutputFormatter = setupAction.OutputFormatters
                    .OfType<JsonOutputFormatter>().FirstOrDefault();

                if (jsonOutputFormatter != null)
                {
                    jsonOutputFormatter.SupportedMediaTypes.Add("application/vnd.marvin.hateoas+json");
                }
                #endregion
            })
            // LOWERCASING  =>   CamelCase
            .AddJsonOptions(options =>          
            {
                options.SerializerSettings.ContractResolver =
                new CamelCasePropertyNamesContractResolver();
            });

            // register the DbContext on the container, getting the connection string from
            // appSettings (note: use this during development; in a production environment,
            // it's better to store the connection string in an environment variable)
            var connectionString = Configuration["connectionStrings:libraryDBConnectionString"];
            services.AddDbContext<LibraryContext>(o => o.UseSqlServer(connectionString));

            #region Register Repository Pattern

            services.AddScoped<ILibraryRepository, LibraryRepository>();

            #endregion

            #region Property Mapping Service for Sorting Data

            services.AddTransient<IPropertyMappingService, PropertyMappingService>();

            #endregion

            #region Shape Resources e.g. http://localhost:6058/api/authors?fields=id i.e. Dynamically Mapping AuthorDTO properties to Entity.Author for Data Shaping on the Controller Actions that do not support Dynamic Sorting

            services.AddTransient<ITypeHelperService, TypeHelperService>();

            #endregion

            #region Generate URIs to previous and next page for PAGINATION

            // UrlHelper requires context in which the action (IActionHelper => GET / POST / DELETE / PUT / PATCH / .. etc) runs
            // Initiate first time its requested
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

            // UrlHelper will generate URIs to an action (IActionHelper => GET / POST / DELETE / PUT / PATCH / .. etc)
            // .AddScoped is used so an instance is created once per request
            services.AddScoped<IUrlHelper, UrlHelper>(implementationFactory =>
            {
                var actionContext = implementationFactory.GetService<IActionContextAccessor>().ActionContext;
                return new UrlHelper(actionContext);
            });
            #endregion

            #region HTTP Cache Header Middleware => Marvin.Cache.Headers    => ETags in Response Headers are used

            services.AddHttpCacheHeaders(
                (expirationModelOptions)
                =>
                {
                    expirationModelOptions.MaxAge = 600;
                },
                (validationModelOptions)
                =>
                {
                    validationModelOptions.AddMustRevalidate = true;
                });

            #endregion

            //  microsoft.aspnetcore.responsecaching => Response caching relating services  => ETags in Response Headers are used
            services.AddResponseCaching();

            #region Rate Limiting and Throttling => How Many Requests per Minute/Hour/etc.

            // registering service
            services.AddMemoryCache();

            //  AspNetCoreRateLimit => 3rd part NuGet to configure limiting
            services.Configure<IpRateLimitOptions>((options) =>
            {
                options.GeneralRules = new System.Collections.Generic.List<RateLimitRule>()
                {
                    new RateLimitRule()
                    {
                        Endpoint = "*",
                        Limit = 10,
                        Period = "5m"
                    },
                    new RateLimitRule()
                    {
                        Endpoint = "*",
                        Limit = 2,
                        Period = "10s"
                    }
                };
            });
            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
            ILoggerFactory loggerFactory, LibraryContext libraryContext)
        {
            loggerFactory.AddConsole();

            loggerFactory.AddDebug(LogLevel.Information);

            // NLOG must also be configured at Program.cs
            //  loggerFactory.AddProvider(new NLog.Extensions.Logging.NLogLoggerProvider());
            loggerFactory.AddNLog();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                #region GLOBAL Exception Handling   => Below message sends 500 Internal Server Error

                app.UseExceptionHandler(appBuilder => {
                    appBuilder.Run(async context => 
                    {
                        var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();
                        if (exceptionHandlerFeature != null)
                        {
                            var logger = loggerFactory.CreateLogger("Global exception logger");
                                        //  500 is the error code for 500 INTERNAL SERVER ERROR, but it can be anything as well
                            logger.LogError(500,
                                exceptionHandlerFeature.Error,
                                exceptionHandlerFeature.Error.Message);
                        }
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("An unexpected fault happened. Try again later.");
                    });
                });
                #endregion
            }

            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Entities.Author, Models.AuthorDto>()
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => // From Database to User    =>  Get Request
                    $"{src.FirstName} {src.LastName}"))                     // From Database to User    =>  Get Request
                    .ForMember(dest => dest.Age, opt => opt.MapFrom(src =>  // From Database to User    =>  Get Request
                    src.DateOfBirth.GetCurrentAge(src.DateOfDeath)));       // From Database to User    =>  Get Request

                cfg.CreateMap<Entities.Book, Models.BookDto>();

                cfg.CreateMap<Models.AuthorForCreationDto, Entities.Author>();

                cfg.CreateMap<Models.AuthorForCreationWithDateOfDeathDto, Entities.Author>();

                cfg.CreateMap<Models.BookForCreationDto, Entities.Book>();

                cfg.CreateMap<Models.BookForUpdateDto, Entities.Book>();

                cfg.CreateMap<Entities.Book, Models.BookForUpdateDto>();
            });

            // Initial data is always added when application starts
            libraryContext.EnsureSeedDataForContext();

            //  Rate Limiting and Throttling => How Many Requests per Minute/Hour/etc.
            app.UseIpRateLimiting();

            //  microsoft.aspnetcore.responsecaching => Response caching relating services  => ETags in Response Headers are used
            app.UseResponseCaching();

            // HTTP Cache Header Middleware => Marvin.Cache.Headers => ETags in Response Headers are used
            app.UseHttpCacheHeaders();

            app.UseMvc();
        }
    }
}
