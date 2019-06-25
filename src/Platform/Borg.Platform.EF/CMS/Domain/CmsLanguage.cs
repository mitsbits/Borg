using Borg.Framework.Cms.BuildingBlocks;
using Borg.Framework.EF.Instructions.Attributes.Schema;
using Borg.Infrastructure.Core.DDD.Contracts;

namespace Borg.Platform.EF.CMS.Domain
{
    [PlatformDBAggregateRoot(Plural = "Languages", Singular = "Language")]
    [PrimaryKeySequenceDefinition]
    public class CmsLanguage : Entity<int>, IGlobalizationSilo
    {
        public string Title { get; set; }

        [IndexDefinition(IndexName = "IX_TwoLetterISO")]
        public string TwoLetterISO { get; set; }

        public string CultureName { get; set; }
    }
}