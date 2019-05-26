using Borg.Framework.EF.Contracts;
using Borg.Framework.EF.DAL;
using Borg.Platform.EF;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
   public static class ServiceCollectionExteneions
    {
        public static IServiceCollection AddCmsCore(this IServiceCollection services, ILoggerFactory loggerFactory,  IConfiguration configuration)
        {
            services.AddScoped(p => new BorgPlatformDb(loggerFactory, configuration));
            services.AddScoped<IUnitOfWork<BorgPlatformDb>, UnitOfWork<BorgPlatformDb>>();
            return services;
        }
    }
}
