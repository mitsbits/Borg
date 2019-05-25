using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Borg.Framework.EF.Contracts;
using Borg.System.Licencing;
using Borg.System.Licencing.Contracts;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Borg.Web.Clients.Razor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            IBorgLicenceService borgLicenceService = new MemoryMoqLicenceService();
            Seed(host);
            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();

        private static void Seed(IWebHost host)
        {
            IServiceScopeFactory services = host.Services.GetService<IServiceScopeFactory>();
            using (var scope = services.CreateScope())
            {
                var seeds = scope.ServiceProvider.GetServices<IDbSeed>();
                foreach (var seed in seeds)
                {
                    AsyncHelpers.RunSync(() => seed.Run(default));
                }

                var recipes = scope.ServiceProvider.GetServices<IDbRecipe>();
                foreach (var recipe in recipes)
                {
                    AsyncHelpers.RunSync(() => recipe.Run(default));
                }
            }
        }
    }
}
