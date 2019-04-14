using Borg.System.Backoffice.Security;
using Microsoft.Extensions.Options;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExteneions
    {
        public static IServiceCollection AddPolicies(this IServiceCollection services)
        {
            services.AddSingleton<IPostConfigureOptions<BorgAuthenticationOptions>, BorgAuthenticationPostConfigureOptions>();
            services.AddAuthorization(options =>
            {
                options.AddPolicy(BorgConstants.BackofficePolicyName,
                    policy =>
                    {
                        policy.AuthenticationSchemes.Add(BorgConstants.BackofficePolicyName);
                        policy.RequireAuthenticatedUser();
                        policy.Requirements.Add(new BorgRequirement());
                    });
            });

            services.AddAuthentication()
                //.AddScheme<BorgAuthenticationOptions, BorgAuthenticationHandler>(BorgConstants.BackofficePolicyName, null)
                .AddCookie(BorgConstants.BackofficePolicyName, options =>
                {
                    options.LoginPath = "/Backoffice/Account/Login/";
                    options.AccessDeniedPath = "/Backoffice/Account/Forbidden/";
                    options.Cookie.Name = $"Borg{BorgConstants.BackofficePolicyName}";
                    options.ReturnUrlParameter = "returnurl";
                })
                ; ;
            return services;
        }
    }
}