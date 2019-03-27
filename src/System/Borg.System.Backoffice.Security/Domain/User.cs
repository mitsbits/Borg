using Borg.System.DDD.Contracts;

namespace Borg.System.Backoffice.Security.Domain
{
    public class User
    {
    }

    public class Role
    {
    }

    public class UserPermission : PermissionBase
    {
    }

    public class RolePermission : PermissionBase
    {
    }

    public abstract class PermissionBase
    { }

    public abstract class UserBase : IEntity<int>, IHasEmail, IHasPassword
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
    }
}