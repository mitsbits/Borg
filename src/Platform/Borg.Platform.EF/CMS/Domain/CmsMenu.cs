using Borg.Framework.Cms.Annotations;
using Borg.Framework.Cms.BuildingBlocks;
using Borg.Framework.EF.Instructions.Attributes.Schema;
using Borg.Infrastructure.Core.DDD.Contracts;

namespace Borg.Platform.EF.CMS.Domain
{
    [PlatformDBAggregateRoot(Plural = "Menus", Singular = "Menu")]
    [PrimaryKeySequenceDefinition]
    public class CmsMenu : MultilingualEntity<int, CmsLanguage>, IHaveTitle
    {
        public string Title { get; protected set; }
    }

    [CmsAggregateRoot(Plural = "Menus", Singular = "Menu")]
    [PrimaryKeySequenceDefinition]
    public class CmsMenuItem : MultilingualTreeNodeEntity<int, CmsLanguage>, IHaveTitle
    {
        public string Title { get; protected set; }
        public int MenuId { get; protected set; }
        public virtual CmsMenu Menu { get; protected set; }
    }
}