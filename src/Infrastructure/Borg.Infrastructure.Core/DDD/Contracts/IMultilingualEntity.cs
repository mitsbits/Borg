using System;

namespace Borg.Infrastructure.Core.DDD.Contracts
{
    public interface IMultilingualEntity<TKey> : IEntity<TKey>, IHaveLanguage where TKey : IEquatable<TKey>
    {

    }
}
