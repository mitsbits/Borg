using System;

namespace Borg.System.DDD.Contracts
{
    public interface IAgregateRoot<out TKey> : IAgregateRoot, IEntity<TKey> where TKey : IEquatable<TKey>
    {
    }

    public interface IAgregateRoot
    {
        int Version { get; }
    }
}