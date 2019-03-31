using Borg.Infrastructure.Core.DDD.ValueObjects;
using System;

namespace Borg.Infrastructure.Core.DDD.Contracts
{
    public interface IHasCompositeKey<T> where T : IEquatable<T>
    {
        CompositeKey<T> CompositeKey { get; }
    }
}