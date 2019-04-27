using Borg.Infrastructure.Core;
using Borg.Platform.Backoffice.Security.EF;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Borg
{
    public static class DomainExtensions
    {
        public static IEnumerable<Claim> ToClaims(this CmsUser cmsUser)
        {
            Preconditions.NotNull(cmsUser, nameof(cmsUser));
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, cmsUser.Name),
                new Claim(ClaimTypes.Surname, cmsUser.Surname),
                new Claim(ClaimTypes.Email, cmsUser.Email)
            };
            foreach (var role in cmsUser.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Role.Title));
                foreach (var roleClaim in role.Role.Permissions)
                {
                    claims.AddOrReplace(roleClaim.Resource, roleClaim.PermissionOperation.ToString());
                }
            }
            foreach (var userClaim in cmsUser.Permissions)
            {
                claims.AddOrReplace(userClaim.Resource, userClaim.PermissionOperation.ToString());
            }
            return claims;
        }

        private static void AddOrReplace(this List<Claim> collection, string claimType, string value)
        {
            if (collection.All(x => x.Type != claimType))
            {
                collection.Add(new Claim(claimType, value));
            }
        }
    }
}