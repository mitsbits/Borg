using Borg.Framework.Cms.BuildingBlocks;
using Borg.Framework.EF.Instructions.Attributes.Schema;
using Borg.Infrastructure.Core.DDD.Contracts;

namespace Borg.Platform.EF.CMS.Domain
{
    [PlatformDBAggregateRoot(Plural = "Pages", Singular = "Page")]

    public class CmsPage : CmsMultilingualTreeNode, IHaveTitle, IHaveSlug
    {
        [Unicode]
        public string Title { get; set; }
        public string Slug { get; set; }
    }
}