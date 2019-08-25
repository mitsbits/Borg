using Borg.Framework.EF.Instructions.Attributes.Schema;
using Borg.Infrastructure.Core.DDD.Contracts;
using Borg.Infrastructure.Core.DDD.ValueObjects;
using System;

namespace Borg.Platform.EF.CMS.Domain
{
    public abstract class CmsEntity : IEntity<int>
    {
        [PrimaryKeySequenceDefinition]
        public virtual int Id { get; protected set; }

        public virtual CompositeKey Keys => CompositeKeyInternal();

        protected virtual CompositeKey CompositeKeyInternal()
        {
            return CompositeKeyBuilder.Create()
                .AddKey(nameof(Id)).AddValue(Id)
                .Build();
        }

        public override string ToString()
        {
            return $"{GetType().Name}:{Keys}";
        }
    }

    public abstract class CmsTreeNode : IMultilingualTreeNode<int, CmsLanguage>
    {
        [IndexSequenceDefinition]
        [PrimaryKeyDefinition]
        public virtual int Id { get; protected set; }

        [PrimaryKeyDefinition]
        public int LanguageID { get; protected set; }

        public virtual CompositeKey Keys => CompositeKeyInternal();

        public int ParentId { get; protected set; } = 0;

        public int Depth { get; protected set; } = 0;

        public string Hierarchy { get; protected set; }

        public CmsLanguage Language => throw new NotImplementedException();

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