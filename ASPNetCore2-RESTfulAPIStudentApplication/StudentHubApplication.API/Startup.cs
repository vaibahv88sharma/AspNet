using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;
using NLog.Extensions.Logging;
using StudentHubApplication.API.Entities;
using StudentHubApplication.API.Helpers;
using StudentHubApplication.API.Services;

namespace StudentHubApplication.API
{
    public class Startup
    {
        public static IConfiguration Configuration { get; private set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        //public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(
                setupAction =>
                {
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

            // Register DBContext
            var connectionString = Startup.Configuration["connectionStrings:applicationInfoDBConnectionString"];
            services.AddDbContext<ApplicationInfoContext>(o => o.UseSqlServer(connectionString));

            services.AddTransient<IApplicationInfoRepository, ApplicationInfoRepository>();
            //services.AddTransient<IApplicationInfoRepository, MockApplicationInfoRepository>();

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
            ILoggerFactory loggerFactory)
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

            //applicationInfoContext.EnsureSeedDataForContext();

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
            });
            
            applicationInfoContext.EnsureSeedDataForContext();

            app.UseMvc();
        }
    }
}
