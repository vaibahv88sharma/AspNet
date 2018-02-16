using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OdeToFood.Data;
using OdeToFood.Services;

namespace OdeToFood
{
    public class Startup
    {
        private IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // implementing OIDC - Azure Authentication -- Cookie Authentication
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
            .AddOpenIdConnect(options =>
            {
                _configuration.Bind("AzureAd", options);
            })
            .AddCookie();

            // Dependency Injection
            services.AddSingleton<IGreeter, Greeter>();
            services.AddDbContext<OdeToFoodDbContext>(options => options.UseSqlServer(_configuration.GetConnectionString("OdeToFood")));
            services.AddScoped<IRestaurantData, SqlRestaurantData>();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, 
            IHostingEnvironment env, 
            IGreeter greeter,
            //IConfiguration configuration
            ILogger<Startup> logger
            )
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            #region Custom Middleware

            //////////////////////////////////// Custom Middleware ////////////////////////////////////////////////
            ////app.Use(next => 
            ////{
            ////    return async context =>
            ////    {
            ////        logger.LogInformation("Request incoming");
            ////        if (context.Request.Path.StartsWithSegments("/mym"))
            ////        {
            ////            await context.Response.WriteAsync("Request Handled");
            ////            logger.LogInformation("Request incoming");
            ////        }
            ////        else
            ////        {
            ////            await next(context);
            ////            logger.LogInformation("Response ongoing");
            ////        }
            ////    };
            ////});
            #endregion

            #region BuiltIn Middleware for Welcome Page

            //////////////////////////////////// BuiltIn Middleware ////////////////////////////////////////////////
            ////app.UseWelcomePage(new WelcomePageOptions {
            ////    Path="/wp"
            ////});
            #endregion

            // Always redirect application to SSL or HTTPS
            app.UseRewriter(new RewriteOptions().AddRedirectToHttpsPermanent());

            #region Use Static Files from www directory including html file. E.g. http://localhost:60098/index.html

            app.UseStaticFiles();
            #endregion

            // use node_modules folder
            //env.ContentRootPath => Absolute path of current project
            app.UseNodeModules(env.ContentRootPath);

            // Authentication will apply to users for Middlewares below this section e.g.  app.UseMvc, app.Run, etc and will not apply to above including app.UseStaticFiles()
            app.UseAuthentication();

            app.UseMvc(ConfigureRoutes);

            //app.Run(async (context) =>
            //{
            //    var greeting = greeter.GetMessageOfTheDay();
            //    context.Response.ContentType = "text/plain";
            //    //await context.Response.WriteAsync($"{greeting}    :   {env.EnvironmentName}");
            //    await context.Response.WriteAsync($"Not found");
            //});
        }

        private void ConfigureRoutes(IRouteBuilder routeBuilder)
        {
            #region Convention based Routing
            //  /Home/Index/4
            // Default Controller // Default Index // id? => optional
            routeBuilder.MapRoute("Default", "{controller=Home}/{action=Index}/{id?}");
            #endregion
        }
    }
}
