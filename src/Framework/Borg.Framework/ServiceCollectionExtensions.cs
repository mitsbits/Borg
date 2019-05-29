using Borg.Framework;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServiceLocator(this IServiceCollection services)
        {
            services.AddSingleton<ServiceLocator>();
            ServiceLocator.SetLocatorProvider(services.BuildServiceProvider());
            return services;
        }
    }
}