using System;

namespace Borg.Infrastructure.Core.DDD.Contracts
{
    public interface IMultilingualEntity<out TKey, out TLanguage> : IEntity<TKey>, IHaveLanguage<TLanguage> where TKey : IEquatable<TKey> where TLanguage : ILanguage
    {
    }
}