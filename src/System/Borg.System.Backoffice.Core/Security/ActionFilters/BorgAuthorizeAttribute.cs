using Microsoft.AspNetCore.Authorization;

namespace Borg.System.Backoffice.Core.Security.ActionFilters
{
    public class BorgAuthorizeAttribute : AuthorizeAttribute
    {
        protected virtual bool SkipAuthentication { get; } = false;

        public BorgAuthorizeAttribute()
        {
            if (!SkipAuthentication)
            {
                Policy = BorgSecurityConstants.BackofficePolicyName;
            }
        }

    }
}