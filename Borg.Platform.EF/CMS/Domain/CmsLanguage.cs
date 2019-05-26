using Borg.Framework.Cms.Annotations;
using Borg.Framework.Cms.BuildingBlocks;
using Borg.Framework.EF.Instructions.Attributes;
using Borg.Infrastructure.Core.DDD.Contracts;

namespace Borg.Platform.EF.CMS.Domain
{
    [CmsEntity(Plural = "Languages", Singular = "Language")]
    [KeySequenceDefinition]
    public class CmsLanguage : Entity<int>, ILanguage
    {
        public string Title { get; set; }

        [UniqueIndexDefinition]
        public string TwoLetterISO { get; set; }
    }
}