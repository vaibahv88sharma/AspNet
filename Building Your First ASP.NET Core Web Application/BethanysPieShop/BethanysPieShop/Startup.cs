using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using BethanysPieShop.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace BethanysPieShop
{
    public class Startup
    {

        //public Startup(IHostingEnvironment hostingEnvironment)
        //{
        //    _configurationRoot = new ConfigurationBuilder()
        //       .SetBasePath(hostingEnvironment.ContentRootPath)
        //       .AddJsonFile("appsettings.json")
        //       //.AddJsonFile($"appsettings.{hostingEnvironment.EnvironmentName}.json", true)
        //       .Build();
        //}
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            }
            );
            //services.AddDbContext<AppDbContext>(options =>
            //                             options.UseSqlServer(_configurationRoot.GetConnectionString("DefaultConnection")));

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>();

            services.AddTransient<ICategoryRepository, CategoryRepository>(); // Dummy Data From Local Class
            services.AddTransient<IPieRepository, PieRepository>(); // Dummy Data From Local Class
                                                                    //services.AddTransient<ICategoryRepository, MockCategoryRepository>(); // Dummy Data From Local Class
                                                                    //services.AddTransient<IPieRepository, MockPieRepository>(); // Dummy Data From Local Class
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<ShoppingCart>(sp => ShoppingCart.GetCart(sp));
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddMvc();

            services.AddMemoryCache();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        //{
        //    app.UseDeveloperExceptionPage();
        //    app.UseStatusCodePages();
        //    app.UseStaticFiles();
        //    app.UseMvcWithDefaultRoute();

        //    DbInitializer.Seed(app);
        //}
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseSession();

            app.UseAuthentication();
            //app.UseIdentity();            //Depricated

            //app.UseMvcWithDefaultRoute();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "categoryfilter",
                    template: "Pie/{action}/{category?}",
                    defaults: new { Controller = "Pie", action = "List" });

                //routes.MapRoute(
                //    name: null,
                //    template: "{category}",
                //    defaults: new { controller = "Pie", action = "List" }
                //);

                routes.MapRoute(
                name: "default",
                template: "{controller=Home}/{action=Index}/{id?}");


            });

            var dbContext = serviceProvider.GetService<AppDbContext>();

            DbInitializer.Seed(app, dbContext);
        }
    }
}
