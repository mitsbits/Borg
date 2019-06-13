using Borg.Framework;
using Borg.System.Backoffice.Security;
using Microsoft.AspNetCore.Http;
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
                options.AddPolicy(BorgSecurityConstants.BackofficePolicyName,
                    policy =>
                    {
                        policy.AuthenticationSchemes.Add(BorgSecurityConstants.BackofficePolicyName);
                        policy.RequireAuthenticatedUser();
                        policy.Requirements.Add(new BorgRequirement());
                    });
            });

            services.AddAuthentication(BorgSecurityConstants.BackofficePolicyName)
                //.AddScheme<BorgAuthenticationOptions, BorgAuthenticationHandler>(BorgConstants.BackofficePolicyName, null)
                .AddCookie(BorgSecurityConstants.BackofficePolicyName, options =>
                {
                    options.LoginPath = "/Backoffice/Account/Login/";
                    options.AccessDeniedPath = "/Backoffice/Account/Forbidden/";
                    options.Cookie.Name = $"Borg{BorgSecurityConstants.BackofficePolicyName}";
                    options.Cookie.SameSite = SameSiteMode.None;
                    options.ReturnUrlParameter = "returnurl";
                })
                ; ;
            return services;
        }
    }
}