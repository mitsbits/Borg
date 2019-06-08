using Borg.Framework.EF.Contracts;
using Borg.Framework.EF.DAL;
using Borg.Framework.EF.Discovery;
using Borg.Platform.EF;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExteneions
    {
        public static IServiceCollection AddCmsCore(this IServiceCollection services, ILoggerFactory loggerFactory, IConfiguration configuration)
        {
            services.AddScoped(p => new BorgPlatformDb(loggerFactory, configuration));
            services.AddScoped<IUnitOfWork<BorgPlatformDb>, UnitOfWork<BorgPlatformDb>>();
            services.AddSingleton<IEntitiesExplorer, EntitiesExplorer>();
            return services;
        }
    }
}