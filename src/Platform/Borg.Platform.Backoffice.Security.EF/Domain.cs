using Borg.Framework.Cms.Annotations;
using Borg.Framework.EF.Instructions.Attributes;
using Borg.Infrastructure.Core.DDD.Contracts;
using Borg.Infrastructure.Core.DDD.Enums;
using Borg.Infrastructure.Core.DDD.ValueObjects;
using Borg.Platform.Backoffice.Security.EF.Data;
using Borg.Platform.EF.Instructions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Borg.Platform.Backoffice.Security.EF
{
    [KeySequenceDefinition(Column = nameof(Id))]
    [CmsEntity(Plural = "Cms Users", Singular = "Cms User")]
    public class CmsUser : UserBase
    {
        public ICollection<CmsUserPermission> Permissions { get; set; } = new HashSet<CmsUserPermission>();

        public ICollection<UserRole> Roles { get; set; } = new HashSet<UserRole>();
    }

    [KeySequenceDefinition(Column = nameof(Id))]
    [CmsEntity(Plural = "Cms Roles", Singular = "Cms Role")]
    public class CmsRole : RoleBase
    {
        public ICollection<CmsRolePermission> Permissions { get; set; } = new HashSet<CmsRolePermission>();
        public ICollection<UserRole> Users { get; set; } = new HashSet<UserRole>();
        public bool IsSystem { get; set; }
    }

    [KeySequenceDefinition(Column = nameof(Id))]
    [CmsEntity(Plural = "Cms User Permissions", Singular = "Cms User Permission")]
    public class CmsUserPermission : PermissionBase
    {
        public virtual CmsUser User { get; set; }
    }

    [KeySequenceDefinition(Column = nameof(Id))]
    [CmsEntity(Plural = "Cms Role Permissions", Singular = "Cms Role Permission")]
    public class CmsRolePermission : PermissionBase
    {
        public virtual CmsRole Role { get; set; }
    }

    [Table("CmsUserCmsRole")]
    public class UserRole
    {
        [PrimaryKeyDefinition(order: 1)]
        public int UserId { get; set; }

        [PrimaryKeyDefinition(order: 2)]
        public int RoleId { get; set; }

        public virtual CmsUser User { get; set; }
        public virtual CmsRole Role { get; set; }
    }

    public abstract class PermissionBase : IEntity<int>, ITreeNode<int>, IHavePermissionOperation
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public int Depth { get; set; }
        public string Resource { get; set; }
        public PermissionOperation PermissionOperation { get; set; }

        public CompositeKey Keys
        {
            get
            {
                var keys = new CompositeKey();
                keys.Add(nameof(Id), Id);
                return keys;
            }
        }
    }

    public abstract class UserBase : IEntity<int>, IHavePassword, IPerson, IActive
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public CompositeKey Keys
        {
            get
            {
                var keys = new CompositeKey();
                keys.Add(nameof(Id), Id);
                return keys;
            }
        }
    }

    public abstract class RoleBase : IEntity<int>, IHaveTitle
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public CompositeKey Keys
        {
            get
            {
                var keys = new CompositeKey();
                keys.Add(nameof(Id), Id);
                return keys;
            }
        }
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
            builder.Entity<CmsRole>().HasMany(x => x.Users).WithOne(x => x.Role).HasForeignKey(x => x.RoleId);
        }
    }

    public class CmsUserMap : EntityMap<CmsUser, SecurityDbContext>
    {
        public override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<CmsUser>().HasMany(x => x.Roles).WithOne(x => x.User).HasForeignKey(x => x.UserId);
        }
    }

    public class UserRoleMap : EntityMap<UserRole, SecurityDbContext>
    {
        public override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}