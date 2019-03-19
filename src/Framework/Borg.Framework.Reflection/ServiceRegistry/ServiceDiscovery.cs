using Borg.Infrastructure.Core.DI;
using System.Linq;
using System.Reflection;

namespace Borg.Framework.Reflection.ServiceRegistry
{
    internal class ServiceDiscovery
    {
        private readonly Assembly[] _assemblies;

        internal ServiceDiscovery(Assembly[] assemblies)
        {
            _assemblies = assemblies;
            Discover();
        }

        private void Discover()
        {
            FindAllPlugableServices();
        }

        private void FindAllPlugableServices()
        {
            var servicesTypes = _assemblies.SelectMany(x => x.GetTypes()).Where(x => x.GetCustomAttributes<PlugableServiceAttribute>().Any()).Distinct();
        }
    }
}