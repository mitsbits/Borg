﻿using Borg.Framework.Reflection;
using Borg.System.AddOn;
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
        private readonly ILoggerFactory _loggerFactory;

        public Startup(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IBorgLicenceService>(new Borg.System.Licencing.MemoryMoqLicenceService());
            services.RegisterPlugableServices(_loggerFactory);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2).AddControllersAsServices();
            services.ConfigureOptions(typeof(System.Backoffice.UiConfigureOptions));


            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            var ls = app.ApplicationServices.GetService<IBorgLicenceService>();
          
            app.UseMiddleware(typeof(LicenceMiddleware), ls);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

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