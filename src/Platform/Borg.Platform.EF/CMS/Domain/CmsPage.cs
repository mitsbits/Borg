using Borg.Framework.Cms.BuildingBlocks;
using Borg.Framework.EF.Instructions.Attributes.Schema;
using Borg.Infrastructure.Core.DDD.Contracts;

namespace Borg.Platform.EF.CMS.Domain
{
    [PlatformDBAggregateRoot(Plural = "Pages", Singular = "Page")]
    [IndexSequenceDefinition]
    public class CmsPage : MultilingualTreeNodeEntity<int, CmsLanguage>, IHaveTitle
    {
        public string Title { get; set; }
    }
}