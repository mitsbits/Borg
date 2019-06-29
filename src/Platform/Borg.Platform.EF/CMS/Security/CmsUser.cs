using Borg.Framework.Cms.BuildingBlocks;
using Borg.Framework.EF.Instructions.Attributes.Schema;
using Borg.Infrastructure.Core.DDD.Contracts;
using System.Collections.Generic;

namespace Borg.Platform.EF.CMS.Security
{
    [PrimaryKeySequenceDefinition(Column = nameof(Id))]
    [PlatformDBAggregateRoot(Plural = "Cms Users", Singular = "Cms User")]
    public class CmsUser : UserBase
    {
        public CmsUser(int id) : this()
        {
            Id = id;
        }

        public CmsUser()
        {
            Permissions = new HashSet<CmsUserPermission>();
            Roles = new HashSet<UserRole>();
        }

        public ICollection<CmsUserPermission> Permissions { get; protected set; }

        public ICollection<UserRole> Roles { get; protected set; }
    }

    public abstract class UserBase : Entity<int>, IHavePassword, IPerson, IActive
    {
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}