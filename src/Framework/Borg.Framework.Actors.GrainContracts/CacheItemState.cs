using Borg.Infrastructure.Core.DDD.Contracts;
using System;

namespace Borg.Framework.Actors.GrainContracts
{
    public class CacheItemState<T> : ICacheItemState<T>
    {
        public DateTimeOffset? Expiration { get; set; }

        public T Data { get; set; }
    }

    public interface ICacheItemState<T> : IHaveExpiration
    {
        T Data { get; set; }
    }
}