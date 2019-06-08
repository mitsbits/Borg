using Borg.Infrastructure.Core.DDD.Contracts;
using Borg.Infrastructure.Core.DDD.ValueObjects;
using System;

namespace Borg.Framework.Cms.BuildingBlocks
{
    public abstract class MultilingualEntity<TKey, TLanguage> : IMultilingualEntity<TKey, TLanguage> where TKey : IEquatable<TKey> where TLanguage : ILanguage
    {
        public TKey Id { get; protected set; }
        public virtual CompositeKey Keys => CompositeKeyInternal();
        public virtual string TwoLetterISO { get; protected set; }
        public virtual TLanguage Language { get; protected set; }

        private CompositeKey CompositeKeyInternal()
        {
            return CompositeKeyBuilder.Create()
                .AddKey(nameof(Id)).AddValue(Id)
                .AddKey(nameof(TwoLetterISO)).AddValue(TwoLetterISO)
                .Build();
        }

        public override string ToString()
        {
            return $"{GetType().Name}:{Keys}";
        }
    }
}