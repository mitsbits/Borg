using Borg.Platform.Backoffice.Security.EF;
using Borg.Platform.Backoffice.Security.EF.Data;
using Borg.System.Backoffice.Security;
using Borg.System.Backoffice.Security.Contracts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExteneions
    {
        private static string connectionString = "Data Source=.;Initial Catalog=Borg.Security;User Id=sa;Password=P@ssw0rd;Pooling=False;Max Pool Size=200;MultipleActiveResultSets=True";

        public static IServiceCollection AddCmsUsers(this IServiceCollection services, ILoggerFactory loggerFactory,
            IHostingEnvironment environment)
        {
            services.AddScoped<ICmsUserManager<CmsUser>, CmsUserManager>();
            services.AddSingleton<ICmsUserPasswordValidator, BorgCmsUserPasswordValidator>();
            services.AddDbContext<SecurityDbContext>(options =>
            {
                options.UseSqlServer(connectionString, opt =>
                {
                    opt.EnableRetryOnFailure(3, TimeSpan.FromSeconds(30), new int[0]);
                });
                options.EnableSensitiveDataLogging(environment.IsDevelopment());
            });
            return services;
        }
    }
}