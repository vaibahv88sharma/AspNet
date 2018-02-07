using CityInfo.API.Entities;
using CityInfo.API.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace CityInfo.API
{
    public class Startup
    {
        public static IConfiguration Configuration { get; private set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .AddMvcOptions(o => o.OutputFormatters.Add(
                        new XmlDataContractSerializerOutputFormatter()
                    ));
                //  UpperCase of the parameters returned from API e.g. "Id", "Name"
                //.AddJsonOptions(o => {
                //    if (o.SerializerSettings.ContractResolver != null) {
                //        var castedResolver = o.SerializerSettings.ContractResolver as DefaultContractResolver;
                //        castedResolver.NamingStrategy = null;
                //    }
                //})
                ;
#if DEBUG
            services.AddTransient<IMailService, LocalMailService>();
#else
            services.AddTransient<IMailService, CloudMailService>();
#endif

            var connectionString = Startup.Configuration["connectionStrings:cityInfoDBConnectionString"];
            services.AddDbContext<CityInfoContext>(o => o.UseSqlServer(connectionString));

            services.AddScoped<ICityInfoRepository, CityInfoRepository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, CityInfoContext cityInfoContext)
        {
            loggerFactory.AddConsole();
            loggerFactory.AddDebug();
            //loggerFactory.AddNLog();
            //configure NLog
            loggerFactory.AddNLog(new NLogProviderOptions { CaptureMessageTemplates = true, CaptureMessageProperties = true });
            loggerFactory.ConfigureNLog(@"C:\Users\vsharma.adm\Downloads\4Feb\1\CityInfo.API\CityInfo.API\nlog.config");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            cityInfoContext.EnsureSeedDataForContext();

            app.UseStatusCodePages();
            AutoMapper.Mapper.Initialize(cfg =>
            {
                // PARENT ROUTE
                    // HTTP-Get (Get) All Parents in Parent Route
                    cfg.CreateMap<Entities.City, Models.CityWithoutPointsOfInterestDto>();

                    // HTTP-Get (Get) All Specific Parent and optionally include Child
                    // http://localhost:61614/api/cities/1?includePointsofinterest=true
                    cfg.CreateMap<Entities.City, Models.CityDto>();


                // CHILD ROUTE
                    // HTTP-Get (Get) All Children in Child Route
                    cfg.CreateMap<Entities.PointOfInterest, Models.PointOfInterestDto>();

                    // HTTP-Post (Insert) Single Child Route
                    cfg.CreateMap<Models.PointOfInterestForCreationDto, Entities.PointOfInterest>();

                    // HTTP-Put (Full Update) Single Child Route
                    cfg.CreateMap<Models.PointOfInterestForUpdateDto, Entities.PointOfInterest>();

                    // HTTP-Patch (Partial Update) Single Child Route
                    cfg.CreateMap<Entities.PointOfInterest, Models.PointOfInterestForUpdateDto>();
            });
            app.UseMvc();
            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
        }
    }
}
