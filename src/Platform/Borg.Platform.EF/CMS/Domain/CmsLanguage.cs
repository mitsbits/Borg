using Borg.Framework.Cms.BuildingBlocks;
using Borg.Framework.EF.Instructions.Attributes;
using Borg.Infrastructure.Core.DDD.Contracts;

namespace Borg.Platform.EF.CMS.Domain
{
    [PlatformDBAggregateRoot(Plural = "Languages", Singular = "Language")]
    [KeySequenceDefinition]
    public class CmsLanguage : Entity<int>, ILanguage
    {
        public string Title { get; set; }

        [UniqueIndexDefinition]
        public string TwoLetterISO { get; set; }
    }
}