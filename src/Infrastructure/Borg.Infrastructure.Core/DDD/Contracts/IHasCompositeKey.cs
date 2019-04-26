using Borg.Infrastructure.Core.DDD.ValueObjects;
using System;

namespace Borg.Infrastructure.Core.DDD.Contracts
{
    public interface IHaveCompositeKey<T> where T : IEquatable<T>
    {
        CompositeKey<T> CompositeKey { get; }
    }
}