using Borg.Framework.Reflection.ServiceRegistry;
using Borg.Framework.Reflection.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace Borg.Framework.Reflection
{
    public static class ServiceCollectionEdxtensions
    {
        public static IServiceCollection RegisterPlugableServices(this IServiceCollection services,ILoggerFactory loggerFactory)
        {
            var asmblProvider = new DepedencyAssemblyProvider(loggerFactory);
            new ServiceDiscovery(asmblProvider.GetAssemblies().ToArray(), services);
            return services;
        }
    }
}