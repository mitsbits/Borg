using Borg.Framework.Cms.Annotations;
using Borg.Infrastructure.Core.DDD.Contracts;

namespace Borg.Platform.EF.CMS.Domain
{
    [PlatformDBAggregateRoot(Plural = "Menus", Singular = "Menu")]
    public class CmsMenu : CmsMultilingualEntity, IHaveTitle
    {
        public string Title { get; protected set; }
    }

    [CmsAggregateRoot(Plural = "Menu items", Singular = "Menu item")]
    public class CmsMenuItem : CmsMultilingualTreeNode, IHaveTitle
    {
        public string Title { get; protected set; }
        public int MenuId { get; protected set; }
        public virtual CmsMenu Menu { get; protected set; }
    }
}