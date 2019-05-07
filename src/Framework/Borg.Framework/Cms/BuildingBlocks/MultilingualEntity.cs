using Borg.Infrastructure.Core.DDD.Contracts;
using Borg.Infrastructure.Core.DDD.ValueObjects;
using System;

namespace Borg.Framework.Cms.BuildingBlocks
{
    public abstract class MultilingualEntity<TKey> : IMultilingualEntity<TKey> where TKey : IEquatable<TKey>
    {
        public abstract TKey Id { get; }
        public virtual CompositeKey Keys => CompositeKeyInternal();
        public virtual string TwoLetterISO { get; set; }
        public abstract ILanguage Language { get; }

        private CompositeKey CompositeKeyInternal()
        {
            return CompositeKeyBuilder.Create()
                .AddKey(nameof(Id)).AddValue(Id)
                .AddKey(nameof(TwoLetterISO)).AddValue(TwoLetterISO)
                .Build();
        }
    }
}