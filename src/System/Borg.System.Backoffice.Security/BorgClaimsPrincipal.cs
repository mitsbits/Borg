using System.Security.Claims;
using System.Security.Principal;

namespace Borg.System.Backoffice.Security
{
    public class BorgClaimsPrincipal : ClaimsPrincipal
    {
        public BorgClaimsPrincipal(IIdentity identity) : base(identity)
        {
        }
    }

    public class BorgIdentity : IIdentity
    {
        public string AuthenticationType => "Borg";

        public bool IsAuthenticated => true;

        public string Name => "mitsbits";
    }
}