using Borg.Platform.Backoffice.Security.EF.Data;
using Borg.Platform.EF.Instructions;
using Borg.Platform.EF.Instructions.Attributes;
using Microsoft.EntityFrameworkCore;

namespace Borg.Platform.Backoffice.Security.EF
{
    [KeySequenceDefinition(nameof(Id))]
    public class CmsUser : System.Backoffice.Security.Domain.User
    {
    }

    [KeySequenceDefinition(nameof(Id))]
    public class CmsRole : System.Backoffice.Security.Domain.Role
    {
    }

    [KeySequenceDefinition(nameof(Id))]
    public class CmsUserPermission : System.Backoffice.Security.Domain.UserPermission
    {
    }

    [KeySequenceDefinition(nameof(Id))]
    public class CmsRolePermission : System.Backoffice.Security.Domain.UserPermission
    {
    }

    public class CmsRolePermissionMap : EntityMap<CmsRolePermission, SecurityDbContext>
    {
        public override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

        }
    }

    public class CmsUserPermissionMap : EntityMap<CmsUserPermission, SecurityDbContext>
    {
        public override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
       
        }
    }

    public class CmsRoleMap : EntityMap<CmsRole, SecurityDbContext>
    {
        public override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
 
        }
    }

    public class CmsUserMap : EntityMap<CmsUser, SecurityDbContext>
    {
        public override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<CmsUser>().ForSqlServerHasIndex(x => x.Id).IsUnique();
        }
    }
}