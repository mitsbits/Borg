using Borg.Framework.EF.Contracts;
using Borg.Framework.EF.DAL;
using Borg.Framework.EF.Discovery;
using Borg.Infrastructure.Core.Reflection.Discovery;
using Borg.Platform.EF;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExteneions
    {
        public static IServiceCollection AddCmsCore(this IServiceCollection services, ILoggerFactory loggerFactory, IConfiguration configuration)
        {
            services.AddScoped<BorgPlatformDb>();
            services.AddScoped<IUnitOfWork<BorgPlatformDb>, UnitOfWork<BorgPlatformDb>>();
            services.AddSingleton<IAssemblyExplorer, EntitiesExplorer>();
            return services;
        }
    }
}