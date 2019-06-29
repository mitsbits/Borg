using Borg.Framework.Modularity.Pipelines;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Borg.Web.Clients.Razor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            //IBorgLicenceService borgLicenceService = new MemoryMoqLicenceService();
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
                var startups = scope.ServiceProvider.GetServices<IHostStartUpJob>();
                foreach (var startup in startups)
                {
                    AsyncHelpers.RunSync(async () => await startup.Execute(default));
                }
            }
        }
    }
}