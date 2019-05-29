using Borg.Framework.Cms.Annotations;
using Borg.Framework.Cms.BuildingBlocks;
using Borg.Framework.EF.Instructions.Attributes;
using Borg.Infrastructure.Core.DDD.Contracts;

namespace Borg.Platform.EF.CMS.Domain
{
    [CmsAggregateRoot(Plural = "Menus", Singular = "Menu")]
    [KeySequenceDefinition]
    public class CmsMenu : Entity<int>, IHaveTitle
    {
        public string Title { get; protected set; }
    }

    [CmsAggregateRoot(Plural = "Menus", Singular = "Menu")]
    [KeySequenceDefinition]
    public class CmsMenuItem : MultilingualEntity<int, CmsLanguage>, IHaveTitle
    {
        public string Title { get; protected set; }
    }
}