using Borg.Framework.Reflection.ServiceRegistry;
using Borg.Framework.Reflection.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace icrosoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterPlugableServices(this IServiceCollection services, ILoggerFactory loggerFactory)
        {
            var asmblProvider = new DepedencyAssemblyProvider(loggerFactory);
            new ServiceDiscovery(asmblProvider.GetAssemblies().ToArray(), services);
            return services;
        }
    }
}