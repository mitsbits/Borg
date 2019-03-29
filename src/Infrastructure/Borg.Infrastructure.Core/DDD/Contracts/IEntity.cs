using System;

namespace Borg.Infrastructure.Core.DDD.Contracts
{
    public interface IEntity<out TKey> : IEntity where TKey : IEquatable<TKey>
    {
        TKey Id { get; }
    }

    public interface IEntity
    {
    }
}