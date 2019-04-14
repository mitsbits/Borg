using Borg.Framework.MVC.Middleware.SecurityHeaders;
using Borg.Framework.Reflection;
using Borg.Framework.Reflection.Services;
using Borg.System.Backoffice.Lib;
using Borg.System.Licencing;
using Borg.System.Licencing.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Borg.Web.Client
{
    public class Startup
    {
        private readonly ILoggerFactory loggerFactory;
        private readonly IHostingEnvironment hostingEnvironment;

        public Startup(ILoggerFactory loggerFactory, IHostingEnvironment hostingEnvironment)
        {
            this.loggerFactory = loggerFactory;
            this.hostingEnvironment = hostingEnvironment;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IBorgLicenceService>(new Borg.System.Licencing.MemoryMoqLicenceService());

            services.RegisterPlugableServices(loggerFactory);
            services.AddSingleton<IBorgLicenceService, MemoryMoqLicenceService>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .ConfigureApplicationPartManager(p =>
                p.FeatureProviders.Add(new GenericControllerFeatureProvider(new[] { new DepedencyAssemblyProvider(loggerFactory) })))
                .AddControllersAsServices();
            services.AddPolicies();
            services.AddCmsUsers(loggerFactory, hostingEnvironment);
            services.ConfigureOptions(typeof(System.Backoffice.UiConfigureOptions));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseAuthentication();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSecurityHeadersMiddleware(
                new SecurityHeadersBuilder()
                    .AddDefaultSecurePolicy());

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "areaRoute",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}