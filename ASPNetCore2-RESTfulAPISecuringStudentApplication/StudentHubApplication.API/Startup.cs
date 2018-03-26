using System.Linq;
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
using StudentHubApplication.API.Entities;
using StudentHubApplication.API.Helpers;
using StudentHubApplication.API.Services;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc.Versioning.Conventions;
using System.Collections.Generic;

namespace StudentHubApplication.API
{
    public class Startup
    {
        public static IConfiguration Configuration { get; private set; }
        IHostingEnvironment _env;

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        //public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApiVersioning(cfg=> 
                {
                    cfg.DefaultApiVersion = new ApiVersion(1, 1);
                    cfg.AssumeDefaultVersionWhenUnspecified = true;

                    //  Shows versions in Response Headers => api-supported-versions →1.0, 1.1, 2.0
                    cfg.ReportApiVersions = true;

                    /*
                        WITHOUT the below code we have to use => http://localhost:63293/api/applications?api-version=1.1
                        WITH the below code we have to use => http://localhost:63293/api/applications
                        Header in the request
                        [
                            Accept  : application/json, etc.
                            ver     : 2.0
                        ]
                    */
                        cfg.ApiVersionReader = new HeaderApiVersionReader("ver", "X-Header-Version");

                    // Conversion based API Versioning
                    cfg.Conventions.Controller<Controllers.CourseCampusesForApplicationController>()
                        .HasApiVersion(new ApiVersion(1,0))
                        .HasApiVersion(new ApiVersion(1,1))
                        .HasApiVersion(new ApiVersion(2,0))
                        .Action(m => m.CreateCourseCampusForApplication(
                                default(int), 
                                default(IEnumerable<Models.CourseCampusForApplicationCreationDto>)
                            )
                        )
                            .MapToApiVersion(new ApiVersion(2, 0));
                }
            );

            services.AddCors(
                cfg=> {
                        cfg.AddPolicy("CRUDOperations",policy=> {
                            policy
                                .AllowAnyHeader()
                                .AllowAnyMethod()
                                    //  Below mentiond specifi URL from which POST/PUT, etc. should be allowed
                                    .WithOrigins("http://localhost:63299");
                                    //  OR
                                    //.AllowAnyOrigin();
                        });

                        cfg.AddPolicy("AnyGet", policy => {
                            policy
                                .AllowAnyHeader()
                                .WithMethods("GET")
                                .AllowAnyOrigin();
                        });
                    }
                );

            // Authorization Policy
            services.AddAuthorization(cfg =>
            {
                cfg.AddPolicy("SuperUsers", p => p.RequireClaim("SuperUser", "True"));
            });

            #region Cookie / JWT Token
            var tokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidIssuer = Startup.Configuration["Tokens:Issuer"],

                ValidateAudience = true,
                ValidAudience = Startup.Configuration["Tokens:Audience"],

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(Startup.Configuration["Tokens:Key"])
                    ),

                RequireExpirationTime = false,
                ValidateLifetime = true
                //, ClockSkew = TimeSpan.Zero
            };

            //services.AddAuthentication()//(JwtBearerDefaults.AuthenticationScheme)//(CookieAuthenticationDefaults.AuthenticationScheme)
            services.AddAuthentication(options =>
            {
                //options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                //options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }
                )
                .AddCookie(options =>
                {
                    //options.Events.OnRedirectToLogin = (ctx) =>
                    //{
                    //    if (ctx.Request.Path.StartsWithSegments("/api") && ctx.Response.StatusCode == 200)
                    //    {
                    //        ctx.Response.StatusCode = 401;
                    //    }
                    //    return Task.CompletedTask;
                    //};
                    //options.Events.OnRedirectToAccessDenied = (ctx) =>
                    //{
                    //    if (ctx.Request.Path.StartsWithSegments("/api") && ctx.Response.StatusCode == 200)
                    //    {
                    //        ctx.Response.StatusCode = 403;
                    //    }
                    //    return Task.CompletedTask;
                    //};
                }
                )
                .AddJwtBearer(cfg =>
                    {
                        cfg.TokenValidationParameters = tokenValidationParameters;
                        cfg.RequireHttpsMetadata = false;
                        cfg.SaveToken = true;
                        cfg.TokenValidationParameters = new TokenValidationParameters()
                        {
                            ValidateIssuer = true,
                            ValidIssuer = Startup.Configuration["Tokens:Issuer"],

                            ValidateAudience = true,
                            ValidAudience = Startup.Configuration["Tokens:Audience"],

                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes(Startup.Configuration["Tokens:Key"])
                            ),

                    //        RequireExpirationTime = false,
                            ValidateLifetime = true
                        };
                    }
                )
            ;

            #endregion

            services.AddMvc(
                setupAction =>
                {
                    #region SSL
                    if (!_env.IsProduction())
                    {
                        setupAction.SslPort = 44302;
                    }
                    setupAction.Filters.Add(new RequireHttpsAttribute());
                    #endregion
                    //setupAction.ReturnHttpNotAcceptable = true;
                    setupAction.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());   //  INPUT => Accept : application/xml
                    setupAction.InputFormatters.Add(new XmlDataContractSerializerInputFormatter());     //  OUTPUT => Content-Type: application/xml

                    #region Registering Input Formatter Media Links
                    var jsonInputFormatter = setupAction.InputFormatters
                    .OfType<JsonInputFormatter>().FirstOrDefault();
                    if (jsonInputFormatter != null)
                    {
                        jsonInputFormatter.SupportedMediaTypes
                        .Add("application/vnd.vaibhav.applications.full+json");
                    }
                    #endregion

                    #region Registering Custom Content Negotiation for Output Formatters => Accept : application/vnd.vaibhav.hateoas+json
                    var jsonOutputFormatter = setupAction.OutputFormatters
                        .OfType<JsonOutputFormatter>().FirstOrDefault();

                    if (jsonOutputFormatter != null)
                    {
                        jsonOutputFormatter.SupportedMediaTypes.Add("application/vnd.vaibhav.hateoas+json");
                    }
                    #endregion
                }
            )
            #region LOWERCASING  of the parameters returned from API e.g. "id", "name"
            .AddJsonOptions(options =>
            {
                options.SerializerSettings.ContractResolver =
                new CamelCasePropertyNamesContractResolver();

                //options.SerializerSettings.ReferenceLoopHandling =
                //  ReferenceLoopHandling.Ignore;
            })
            #endregion

            #region UPPERCASE of the parameters returned from API e.g. "Id", "Name"
            //.AddJsonOptions(o =>
            //{
            //    if (o.SerializerSettings.ContractResolver != null)
            //    {
            //        var castedResolver = o.SerializerSettings.ContractResolver as DefaultContractResolver;
            //        castedResolver.NamingStrategy = null;
            //    }
            //})
            #endregion
            ;

            #region Preprocessor Directive Visual Studio
#if DEBUG
            services.AddTransient<IMailService, LocalMailService>();
#else
            services.AddTransient<IMailService, CloudMailService>();
#endif
            #endregion

            services.AddMemoryCache();

            // Register DBContext
            var connectionString = Startup.Configuration["connectionStrings:applicationInfoDBConnectionString"];
            services.AddDbContext<ApplicationInfoContext>(o => o.UseSqlServer(connectionString));
            //.AddIdentity<CampUser, IdentityRole>();
            services.AddIdentity<CampUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationInfoContext>()
                .AddDefaultTokenProviders()
                ;

            #region Implementing and Securing an API with ASPNET Core => <PackageReference Include="AutoMapper.Extensions.Microsoft.DependencyInjection" Version="3.2.0" />
            //services.AddAutoMapper();
            #endregion

            services.AddTransient<IApplicationInfoRepository, ApplicationInfoRepository>();
            //services.AddTransient<IApplicationInfoRepository, MockApplicationInfoRepository>();
            services.AddScoped<ICampRepository, CampRepository>();

            services.AddTransient<CampIdentityInitializer>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            #region Property Mapping Service for Sorting Data

            services.AddTransient<IPropertyMappingService, PropertyMappingService>();

            #endregion

            #region Shape Resources e.g. http://localhost:6058/api/applications?fields=id i.e. Dynamically Mapping ApplicationDto properties to Entity.Author for Data Shaping on the Controller Actions that do not support Dynamic Sorting

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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, 
            ApplicationInfoContext applicationInfoContext,
            ILoggerFactory loggerFactory,
            CampIdentityInitializer identitySeeder
            )
        {
            loggerFactory.AddConsole();

            loggerFactory.AddDebug(LogLevel.Information);

            // NLOG must also be configured at Program.cs
            //  loggerFactory.AddProvider(new NLog.Extensions.Logging.NLogLoggerProvider());
            loggerFactory.AddNLog();

            //app.UseCors(cfg => {
            //    cfg.AllowAnyHeader()
            //    .AllowAnyMethod()
            //        .AllowAnyOrigin()
            //        //  OR
            //        .WithOrigins("http://URL-Of-Website-from-which-this-API-will-be-accessed");
            //});

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

            //applicationInfoContext.EnsureSeedDataForContext();

            #region Building a RESTful API with ASP.NET Core => <PackageReference Include="AutoMapper" Version="6.2.2" />
            AutoMapper.Mapper.Initialize(cfg => {
                cfg.CreateMap<Entities.Application, Models.ApplicationDto>()
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src =>
                       $"{ src.FirstName} {src.LastName}"))
                    .ForMember(dest => dest.Age, opt => opt.MapFrom(src =>
                        src.DateOfBirth.GetCurrentAge()));
                        //src.DateOfBirth.GetCurrentAge(src.DateOfDeath)));
                cfg.CreateMap<Entities.Country, Models.CountryDto>();
                cfg.CreateMap<Entities.Qualification, Models.QualificationDto>();
                cfg.CreateMap<Models.QualificationForQualificationCreationDto, Entities.Qualification>();
                cfg.CreateMap<Entities.ApplicationQualification, Models.ApplicationQualificationDto>();
                cfg.CreateMap<Entities.Course, Models.CourseDto>();
                cfg.CreateMap<Entities.Campus, Models.CampusDto>();
                cfg.CreateMap<Entities.CourseCampus, Models.CourseCampusDto>();
                cfg.CreateMap<Entities.ApplicationCourseCampus, Models.ApplicationCourseCampusDto>();
                //cfg.CreateMap<Models.ApplicationCourseCampusForApplicationCreationDto, Entities.CourseCampus>();
                //cfg.CreateMap<Models.CampusForCourseApplicationCampusCreationDto, Entities.Campus>();
                //cfg.CreateMap<Models.CourseForCourseApplicationCampusCreationDto, Entities.Course>();
                cfg.CreateMap<Entities.Campus, Models.CampusDto>();
                cfg.CreateMap<Entities.Campus, Models.CampusDto>();
                cfg.CreateMap<Models.ApplicationForCreationDto, Entities.Application>();
                //cfg.CreateMap<Models.CountryForApplicationDto, Entities.Country>();
                cfg.CreateMap<Models.CourseCampusForApplicationUpdateDto, Entities.ApplicationCourseCampus>();
                cfg.CreateMap<Models.CourseCampusForApplicationCreationDto, Entities.ApplicationCourseCampus>();
                cfg.CreateMap<Models.QualificationForApplicationCreationDto, Entities.ApplicationQualification>();

                cfg.CreateMap<Entities.Camp, Models.CampModel>()
                    .ForMember(dest => dest.StartDate,opt => opt.MapFrom(src => src.EventDate))
                    .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EventDate.AddDays(src.Length-10)))
                    .ForMember(dest => dest.Url, opt => opt.ResolveUsing<Models.CampUrlResolver>());
                    //.ForMember(dest => dest.Url, opt => opt.ResolveUsing((camp, model, usunsed, ctx) =>
                    // {
                    //     var url = (IUrlHelper)ctx.Items["UrlHelper"];
                    //     return url.Link("CampGet", new { id = camp.Id });
                    // }));
                
                cfg.CreateMap<Entities.ApplicationQualification, Models._2ndVersionApplicationQualificationDto>()
                    .IncludeBase<Entities.ApplicationQualification, Models.ApplicationQualificationDto>()
                    .ForMember(dest => dest.ApplicationQualificationIdIsPrimary, 
                                    opt => opt.ResolveUsing(
                                        aq => $"ApplicationId - {aq.ApplicationId} and QualificationId - {aq.QualificationId} has been marked Primary Qualification as - {aq.IsPrimaryQualification}" 
                                    )
                                );
            });
            #endregion
            //applicationInfoContext.EnsureSeedDataForContext();
            identitySeeder.Seed().Wait();

            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
