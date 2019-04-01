using Borg.System.Licencing;
using Borg.System.Licencing.Contracts;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Borg.Web.Client
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            IBorgLicenceService borgLicenceService = new MemoryMoqLicenceService();
            
            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}