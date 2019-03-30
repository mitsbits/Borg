using Borg.Infrastructure.Core.DDD.Contracts;
using Borg.Infrastructure.Core.DDD.Enums;
using System.Collections.Generic;

namespace Borg.System.Backoffice.Security.Domain
{
    public class User : UserBase
    {
        ICollection<UserPermission> Permissions { get; set; } = new HashSet<UserPermission>();
    }

    public class Role : RoleBase
    {
        ICollection<RolePermission> Permissions { get; set; } = new HashSet<RolePermission>();
    }

    public class UserPermission : PermissionBase
    {
    }

    public class RolePermission : PermissionBase
    {
    }

    public abstract class PermissionBase : IEntity<int>, ITreeNode<int>, IHasPermissionOperation
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public int Depth { get; set; }
        public string Resource { get; set; }
        public PermissionOperation PermissionOperation { get; set; }
    }

    public abstract class UserBase : IEntity<int>, IHasPassword, IPerson, IActive
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string SurName { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }

    public abstract class RoleBase : IEntity<int>, IHasTitle
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }
}