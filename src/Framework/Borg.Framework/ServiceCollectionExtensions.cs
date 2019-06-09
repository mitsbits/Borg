using Borg.Framework;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public  static partial class ServiceCollectionExtensions
    {
        public static IServiceProvider AddServiceLocator(this IServiceCollection services)
        {
            services.AddSingleton<ServiceLocator>();
            var locator = services.BuildServiceProvider();
            ServiceLocator.SetLocatorProvider(services.BuildServiceProvider());
            return locator;
        }
    }
}