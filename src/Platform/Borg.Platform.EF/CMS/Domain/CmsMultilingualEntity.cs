using Borg.Framework.EF.Instructions.Attributes.Schema;
using Borg.Infrastructure.Core.DDD.Contracts;
using Borg.Infrastructure.Core.DDD.ValueObjects;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Borg.Platform.EF.CMS.Domain
{
    public abstract class CmsMultilingualEntity : IMultilingualEntity<int, CmsLanguage>
    {
        [IndexSequenceDefinition]
        [PrimaryKeyDefinition]
        public virtual int Id { get; protected set; }

        public virtual CompositeKey Keys => CompositeKeyInternal();

        [PrimaryKeyDefinition]
        public virtual int LanguageID { get; protected set; }

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