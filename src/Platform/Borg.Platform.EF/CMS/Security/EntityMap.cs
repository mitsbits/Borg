using Microsoft.EntityFrameworkCore;

namespace Borg.Platform.EF.CMS.Security
{
    public class CmsRoleMap : PlatformDbEntityMap<CmsRole>
    {
        public override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<CmsRole>().HasMany(x => x.Users).WithOne(x => x.Role).HasForeignKey(x => x.RoleId);
        }
    }

    public class CmsUserMap : PlatformDbEntityMap<CmsUser>
    {
        public override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<CmsUser>().HasMany(x => x.Roles).WithOne(x => x.User).HasForeignKey(x => x.UserId);
        }
    }
}