using Borg.Infrastructure.Core.DI;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Borg.Framework.Reflection.ServiceRegistry
{
    internal class ServiceDiscovery
    {
        private readonly List<(Type service, PlugableServiceAttribute[] attrs, Type[] interfaces)> _elligibleServices = new List<(Type service, PlugableServiceAttribute[] attrs, Type[] interfaces)>();

        internal ServiceDiscovery(Assembly[] assemblies, IServiceCollection services)
        {
            Discover(assemblies, services);
        }

        private void Discover(Assembly[] assemblies, IServiceCollection services)
        {
            _elligibleServices.AddRange(FindAllPlugableServices(assemblies));
            foreach (var contract in _elligibleServices.SelectMany(x => x.interfaces).Distinct())
            {
                var registrations = _elligibleServices.Where(x => x.interfaces.Any(i => i == contract));
                RegisterServices(contract, registrations, services);
            }
        }

        private void RegisterServices(Type contract, IEnumerable<(Type service, PlugableServiceAttribute[] attrs, Type[] interfaces)> registrations, IServiceCollection services)
        {
            var isMultipleRegistration = registrations.SelectMany(x => x.attrs.Where(a => a.ImplementationOf == contract)).All(a => a.OneOfMany);
            if (isMultipleRegistration)
            {
                //foreach(regi)
            }
        }

        private IEnumerable<(Type service, PlugableServiceAttribute[] attrs, Type[] interfaces)> FindAllPlugableServices(Assembly[] assemblies)
        {
            return assemblies.SelectMany(x => x.GetTypes())
                .Where(x => !x.IsAbstract && x.GetCustomAttributes<PlugableServiceAttribute>().Any()).Distinct()
                .Select(x => (
                service: x,
                attrs: x.GetCustomAttributes<PlugableServiceAttribute>().ToArray(),
                interfaces: x.GetCustomAttributes<PlugableServiceAttribute>().Select(a => a.ImplementationOf).ToArray()));
        }
    }
}