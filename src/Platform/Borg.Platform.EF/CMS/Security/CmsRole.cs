using Borg.Framework.Cms.BuildingBlocks;
using Borg.Framework.EF.Instructions.Attributes.Schema;
using Borg.Infrastructure.Core.DDD.Contracts;
using System.Collections.Generic;

namespace Borg.Platform.EF.CMS.Security
{
    [PrimaryKeySequenceDefinition(Column = nameof(Id))]
    [PlatformDBAggregateRoot(Plural = "Cms Roles", Singular = "Cms Role")]
    public class CmsRole : RoleBase
    {
        public ICollection<CmsRolePermission> Permissions { get; set; } = new HashSet<CmsRolePermission>();
        public ICollection<UserRole> Users { get; set; } = new HashSet<UserRole>();
        public bool IsSystem { get; set; }
    }

    public abstract class RoleBase : Entity<int>, IHaveTitle
    {
        public string Title { get; set; }
    }
}