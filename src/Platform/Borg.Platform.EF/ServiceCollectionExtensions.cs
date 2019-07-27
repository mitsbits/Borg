using Borg.Framework.EF.Contracts;
using Borg.Framework.EF.DAL;
using Borg.Framework.EF.Discovery;
using Borg.Infrastructure.Core.Reflection.Discovery;
using Borg.Platform.EF;
using Borg.Platform.EF.CMS.Security;
using Borg.System.Backoffice.Security;
using Borg.System.Backoffice.Security.Contracts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Security;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExteneions
    {
        public static IServiceCollection AddCmsCore(this IServiceCollection services, ILoggerFactory loggerFactory, IConfiguration configuration)
        {
            services.AddScoped<BorgDb>();
            services.AddScoped<IUnitOfWork<BorgDb>, UnitOfWork<BorgDb>>();
            services.AddSingleton<IAssemblyExplorer, EntitiesExplorer>();
            return services;
        }

        public static IServiceCollection AddCmsUsers(this IServiceCollection services, ILoggerFactory loggerFactory, IHostingEnvironment environment, IConfiguration configuration)
        {
            services.AddScoped<ICmsUserManager<CmsUser>, CmsUserManager>();
            services.AddSingleton<ICmsUserPasswordValidator, BorgCmsUserPasswordValidator>();
            services.AddScoped<SecurityContext>();

            return services;
        }
    }
}