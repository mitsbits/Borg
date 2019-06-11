using Borg.Framework;
using Borg.Framework.EF.Discovery;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class ServiceCollectionExtensions
    {
        public static IServiceProvider AddServiceLocator(this IServiceCollection services)
        {
            services.AddSingleton<ServiceLocator>();
            var locator = services.BuildServiceProvider();
            ServiceLocator.SetLocatorProvider(locator);
            return locator;
        }

        public static IServiceCollection AddAssemblyExplorerOrchestrator(this IServiceCollection services)
        {
            services.AddSingleton<IAssemblyExplorerOrchestrator, AssemblyExplorerOrchestrator>();
            return services;
        }
    }
}