using Borg.Framework.EF.Discovery;
using Borg.Framework.Modularity;
using Borg.Framework.MVC.Middleware.SecurityHeaders;
using Borg.Framework.MVC.Sevices;
using Borg.Framework.Reflection.Services;

using Borg.Framework.Services.Serializer;
using Borg.Infrastructure.Core.Reflection.Discovery;
using Borg.Infrastructure.Core.Services.Serializer;
using Borg.System.Backoffice.Core.GenericEntity;
using Borg.System.Licencing;
using Borg.System.Licencing.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace Borg.Web.Clients.Razor
{
    public class Startup
    {
        private readonly ILoggerFactory loggerFactory;
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly IConfiguration configuration;
        private AssemblyExplorerResult entitiesExplorerResult;

        public Startup(ILoggerFactory loggerFactory, IHostingEnvironment hostingEnvironment, IConfiguration configuration)
        {
            this.loggerFactory = loggerFactory;
            this.hostingEnvironment = hostingEnvironment;
            this.configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IBorgLicenceService>(new Borg.System.Licencing.MemoryMoqLicenceService());

            var depAsmblPrv = new DepedencyAssemblyProvider(loggerFactory);
            var refAsmblPrv = new ReferenceAssemblyProvider(loggerFactory, null, GetType().Assembly);
            var explorer = new EntitiesExplorer(loggerFactory,
                    new IAssemblyProvider[]
                    {
                                refAsmblPrv,
                                depAsmblPrv
                    });

            entitiesExplorerResult = new AssemblyExplorerResult(loggerFactory, new[] { explorer });

            services.AddSingleton<IAssemblyProvider>(depAsmblPrv);
            services.AddSingleton<IAssemblyProvider>(refAsmblPrv);
            services.AddScoped<IUserSession, UserSession>();
            services.AddDistributedMemoryCache((o) =>
            {

            });
            services.AddHttpContextAccessor();
            services.AddSingleton<IBorgLicenceService, MemoryMoqLicenceService>();
            services.AddSingleton<IConfiguration>(configuration);
            services.AddAssemblyExplorerOrchestrator();
            services.AddPlugableServicesExplorer();
            services.AddDistributedMemoryCache();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .ConfigureApplicationPartManager(p =>
                p.FeatureProviders.Add(new BackOfficeEntityControllerFeatureProvider(entitiesExplorerResult)))
                .AddControllersAsServices();
            services.Configure<RouteOptions>(routeOptions =>
            {
                routeOptions.ConstraintMap.Add("genericController", typeof(BackOfficeEntityControllerConstraint));
            });
            services.AddPolicies();
            services.AddDispatcherNetCore();
            services.AddCmsUsers(loggerFactory, hostingEnvironment, configuration);
            services.AddCmsCore(loggerFactory, configuration);
            services.ConfigureOptions(typeof(System.Backoffice.UiConfigureOptions));
            services.AddGenericInventories(entitiesExplorerResult);
            services.RegisterPlugableServices(loggerFactory, depAsmblPrv, refAsmblPrv);
            return services.AddServiceLocator();
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