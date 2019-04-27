using Borg.Framework.MVC.Middleware.SecurityHeaders;
using Borg.Framework.Reflection;
using Borg.Framework.Reflection.Services;
using Borg.Framework.Services.AssemblyScanner;
using Borg.System.Backoffice.Lib;
using Borg.System.Licencing;
using Borg.System.Licencing.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Borg.Web.Client
{
    public class Startup
    {
        private readonly ILoggerFactory loggerFactory;
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly IConfiguration configuration;

        public Startup(ILoggerFactory loggerFactory, IHostingEnvironment hostingEnvironment, IConfiguration configuration)
        {
            this.loggerFactory = loggerFactory;
            this.hostingEnvironment = hostingEnvironment;
            this.configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IBorgLicenceService>(new Borg.System.Licencing.MemoryMoqLicenceService());

            services.AddSingleton<IAssemblyProvider>(new DepedencyAssemblyProvider(loggerFactory));
            services.AddSingleton<IAssemblyProvider>(new ReferenceAssemblyProvider(loggerFactory, GetType().Assembly));

            services.RegisterPlugableServices(loggerFactory);
            services.AddSingleton<IBorgLicenceService, MemoryMoqLicenceService>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .ConfigureApplicationPartManager(p =>
                p.FeatureProviders.Add(new BackOfficeEntityControllerFeatureProvider(new[] { new DepedencyAssemblyProvider(loggerFactory) })))
                .AddControllersAsServices();
            services.Configure<RouteOptions>(routeOptions =>
            {
                routeOptions.ConstraintMap.Add("genericController", typeof(BackOfficeEntityControllerConstraint));
            });
            services.AddPolicies();
            services.AddDispatcherNetCore();
            services.AddCmsUsers(loggerFactory, hostingEnvironment, configuration);
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
                    name: "areaGenericEntityRoute",
                    template: "{area:exists}/entity/{controller:genericController}/{action=Index}/{id?}");
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