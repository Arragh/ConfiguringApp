using ConfiguringApp.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ConfiguringApp
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<UptimeService>();
            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();

                if ((Configuration.GetSection("ShortCircuitMiddleware") ? .GetValue<bool>("EnableBrowserTypeMiddleware")).Value)
                {
                    app.UseMiddleware<BrowserTypeMiddleware>();
                    app.UseMiddleware<ShortCircuitMiddleware>();
                }

                //app.UseMiddleware<ErrorMiddleware>();
                //app.UseMiddleware<ContentMiddleware>();
            }
            else
            {
                app.UseExceptionHandler("../Home/Error");
            }

            app.UseRouting();
            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(null, "{controller=Home}/{action=Index}/{id?}");

                //endpoints.MapGet("/", async context =>
                //{
                //    await context.Response.WriteAsync("Hello World!");
                //});
            });
        }
    }
}
