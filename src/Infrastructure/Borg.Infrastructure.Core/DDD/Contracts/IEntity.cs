using System;
using System.Collections.Generic;

namespace Borg.Infrastructure.Core.DDD.Contracts
{
    public interface IEntity<out TKey> : IEntity, IIdentifiable where TKey : IEquatable<TKey>
    {
        TKey Id { get; }
    }

    public interface IEntity
    {
    }

    public interface IIdentifiable
    {
        IEnumerable<(string key, object value)> Keys { get; }
    }
}