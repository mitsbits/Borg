using Borg.Infrastructure.Core.DDD.Contracts;

namespace Borg.System.Backoffice.Security.Domain
{
    public class User : UserBase
    {
    }

    public class Role : RoleBase
    {
    }

    public class UserPermission : PermissionBase
    {
    }

    public class RolePermission : PermissionBase
    {
    }

    public abstract class PermissionBase : IEntity<int>
    {
        public int Id { get; set; }
    }

    public abstract class UserBase : IEntity<int>, IHasEmail, IHasPassword
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
    }

    public abstract class RoleBase : IEntity<int>, IHasTitle
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }
}