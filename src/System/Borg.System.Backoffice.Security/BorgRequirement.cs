using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Threading.Tasks;

namespace Borg.System.Backoffice.Security
{
    public class BorgRequirement : AuthorizationHandler<BorgRequirement>, IAuthorizationRequirement
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, BorgRequirement requirement)
        {
            var borgprincipal = context.User.Identities.FirstOrDefault(x => x.AuthenticationType == "Borg");
            if (borgprincipal == null)
            {
                context.Fail();
            }
            else
            {
                if (borgprincipal.IsAuthenticated)
                {
                    context.Succeed(requirement);
                }
                else
                {
                    context.Fail();
                }
            }

            return Task.CompletedTask;
        }
    }
}