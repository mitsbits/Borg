using Microsoft.AspNetCore.Authorization;

namespace Borg.System.Backoffice.Security
{
    public class BorgAuthorizeAttribute : AuthorizeAttribute
    {
        protected virtual bool SkipAuthentication { get; } = false;

        public BorgAuthorizeAttribute()
        {
            if (!SkipAuthentication)
            {
                Policy = BorgConstants.BackofficePolicyName;
            }
        }
    }
}