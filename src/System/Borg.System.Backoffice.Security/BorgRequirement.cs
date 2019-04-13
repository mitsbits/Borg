using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace Borg.System.Backoffice.Security
{
    public class BorgRequirement : AuthorizationHandler<BorgRequirement>, IAuthorizationRequirement
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, BorgRequirement requirement)
        {
            var borgprincipal = context.User as BorgClaimsPrincipal;
            if (borgprincipal == null)
            {
                context.Fail();
            }
            else
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}