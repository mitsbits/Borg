using Borg.System.Backoffice.Security;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExteneions
    {
        public static IServiceCollection AddPolicies(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy(BorgConstants.BackofficePolicyName,
                    policy => policy.Requirements.Add(new BorgRequirement()));
            });

            services.AddAuthentication(BorgConstants.BackofficePolicyName).AddCookie(BorgConstants.BackofficePolicyName, options =>
            {
                options.LoginPath = "/Backoffice/Account/Login/";
                options.AccessDeniedPath = "/Backoffice/Account/Forbidden/";
                options.Cookie.Name = $"Borg{BorgConstants.BackofficePolicyName}";
                options.ReturnUrlParameter = "returnurl";
            });
            return services;
        }
    }
}