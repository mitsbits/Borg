using Borg.Framework.EF.Instructions.Attributes.Schema;
using Borg.Infrastructure.Core.DDD.Contracts;
using Borg.Infrastructure.Core.DDD.ValueObjects;

namespace Borg.Platform.EF.CMS.Domain
{
    public abstract class CmsMultilingualTreeNode : IMultilingualTreeNode<int, CmsLanguage>
    {
        [PrimaryKeyDefinition]
        [SequenceDefinition]
        public int Id { get; protected set; }

        [PrimaryKeyDefinition]
        public int LanguageID { get; set; }

        public virtual CompositeKey Keys => CompositeKeyInternal();

        public int ParentId { get; protected set; } = 0;

        public int Depth { get; protected set; } = 0;

        public string Hierarchy { get; protected set; }

        public virtual CmsLanguage Language { get; protected set; }

        protected virtual CompositeKey CompositeKeyInternal()
        {
            return CompositeKeyBuilder.Create()
                .AddKey(nameof(Id)).AddValue(Id)
                .AddKey(nameof(LanguageID)).AddValue(LanguageID)
                .Build();
        }

        public override string ToString()
        {
            return $"{GetType().Name}:{Keys}";
        }
    }
}