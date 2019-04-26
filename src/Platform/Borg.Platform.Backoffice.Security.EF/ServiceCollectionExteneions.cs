using Borg.Framework.EF.Contracts;
using Borg.Framework.EF.DAL;
using Borg.Platform.Backoffice.Security.EF;
using Borg.Platform.Backoffice.Security.EF.Data;
using Borg.System.Backoffice.Security;
using Borg.System.Backoffice.Security.Contracts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExteneions
    {
        public static IServiceCollection AddCmsUsers(this IServiceCollection services, ILoggerFactory loggerFactory, IHostingEnvironment environment, IConfiguration configuration)
        {
            services.AddScoped<ICmsUserManager<CmsUser>, CmsUserManager>();
            services.AddSingleton<ICmsUserPasswordValidator, BorgCmsUserPasswordValidator>();
            services.AddDbContext<SecurityDbContext>(options =>
            {
                options.UseSqlServer(configuration[$"{nameof(SecurityDbContext)}:ConnectionString"], opt =>
                {
                    opt.EnableRetryOnFailure(3, TimeSpan.FromSeconds(30), new int[0]);
                });
                options.UseLoggerFactory(loggerFactory).EnableDetailedErrors(environment.IsDevelopment()).EnableSensitiveDataLogging(environment.IsDevelopment());
            });
            services.AddScoped<IUnitOfWork<SecurityDbContext>, UnitOfWork<SecurityDbContext>>();
            return services;
        }
    }
}