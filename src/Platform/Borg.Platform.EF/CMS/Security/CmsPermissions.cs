using Borg.Framework.Cms.BuildingBlocks;
using Borg.Infrastructure.Core.DDD.Contracts;
using Borg.Infrastructure.Core.DDD.Enums;

namespace Borg.Platform.EF.CMS.Security
{
    [PlatformDBAggregateRoot(Plural = "Cms User Permissions", Singular = "Cms User Permission")]
    public class CmsUserPermission : PermissionBase
    {
        public virtual CmsUser User { get; set; }
    }

    [PlatformDBAggregateRoot(Plural = "Cms Role Permissions", Singular = "Cms Role Permission")]
    public class CmsRolePermission : PermissionBase
    {
        public virtual CmsRole Role { get; set; }
    }

    public abstract class PermissionBase : TreeNodeEntity<int>, IHavePermissionOperation
    {
        public string Resource { get; set; }
        public PermissionOperation PermissionOperation { get; set; }
    }
}