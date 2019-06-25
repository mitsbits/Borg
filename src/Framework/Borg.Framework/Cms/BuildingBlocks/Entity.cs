using Borg.Infrastructure.Core.DDD.Contracts;
using Borg.Infrastructure.Core.DDD.ValueObjects;
using System;

namespace Borg.Framework.Cms.BuildingBlocks
{
    public abstract class Entity<TKey> : IEntity<TKey> where TKey : IEquatable<TKey>
    {
        public TKey Id { get; protected set; }

        public virtual CompositeKey Keys => CompositeKeyInternal();

        private CompositeKey CompositeKeyInternal()
        {
            return CompositeKeyBuilder.Create()
                .AddKey(nameof(Id)).AddValue(Id)
                .Build();
        }
    }
}