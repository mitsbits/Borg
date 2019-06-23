using Borg.Framework.EF.Instructions.Attributes;
using System.ComponentModel.DataAnnotations.Schema;

namespace Borg.Platform.EF.CMS.Security
{
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
}