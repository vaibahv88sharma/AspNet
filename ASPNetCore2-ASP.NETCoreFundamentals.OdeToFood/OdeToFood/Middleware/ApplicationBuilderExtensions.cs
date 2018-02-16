using Microsoft.Extensions.FileProviders;
using System.IO;

// NAMESPACE is changed to reflect Built-In  Microsoft.AspNetCore.Builder  =>  Any method for 'IApplicationBuilder' must be in Microsoft.AspNetCore.Builder Namespace

namespace Microsoft.AspNetCore.Builder
//namespace OdeToFood.Middleware
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseNodeModules(
            this IApplicationBuilder app, string root)
        {
            var path = Path.Combine(root, "node_modules");
            var fileProvider = new PhysicalFileProvider(path);

            var options = new StaticFileOptions();
            options.RequestPath = "/node_modules";
            options.FileProvider = fileProvider;

            app.UseStaticFiles(options);
            return app;
        }
    }
}
