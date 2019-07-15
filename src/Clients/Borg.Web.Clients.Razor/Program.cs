using Borg.Framework.Modularity.Pipelines;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace Borg.Web.Clients.Razor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            //IBorgLicenceService borgLicenceService = new MemoryMoqLicenceService();
            HostStartUp(host);
            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();

        private static void HostStartUp(IWebHost host)
        {
            IServiceScopeFactory services = host.Services.GetService<IServiceScopeFactory>();
            using (var scope = services.CreateScope())
            {
                var startups = scope.ServiceProvider.GetServices<IHostStartUpJob>();
                foreach (var startup in startups)
                {
                    AsyncHelpers.RunSync(async () => await startup.Execute(default));
                }
            }
        }
    }
}