using Borg.Framework.EF.Contracts;
using Borg.Framework.EF.DAL;
using Borg.Platform.Backoffice.Security.EF;
using Borg.Platform.Backoffice.Security.EF.Data;
using Borg.System.Backoffice.Security;
using Borg.System.Backoffice.Security.Contracts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExteneions
    {
        public static IServiceCollection AddCmsUsers(this IServiceCollection services, ILoggerFactory loggerFactory, IHostingEnvironment environment, IConfiguration configuration)
        {
            services.AddScoped<ICmsUserManager<CmsUser>, CmsUserManager>();
            services.AddSingleton<ICmsUserPasswordValidator, BorgCmsUserPasswordValidator>();
            services.AddScoped(p => new SecurityDbContext(loggerFactory, configuration));

            services.AddScoped<IUnitOfWork<SecurityDbContext>, UnitOfWork<SecurityDbContext>>();
            return services;
        }
    }
}