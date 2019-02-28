using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using System.Threading;
using System.Threading.Tasks;

namespace Borg.Framework.MVC.Cache
{
    public class InMemoryDistributedCacheWrapper : IDistributedCache
    {
        private readonly IMemoryCache _memoryCache;

        public InMemoryDistributedCacheWrapper(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public byte[] Get(string key)
        {
            var hit = _memoryCache.Get(key);
            return hit as byte[];
        }

        public Task<byte[]> GetAsync(string key, CancellationToken token = default(CancellationToken))
        {
            var task = _memoryCache.GetOrCreateAsync<byte[]>(key, null);
            return task;
        }

        public void Refresh(string key)
        {
            var value = _memoryCache.Get(key);
            _memoryCache.Set(key, value);
        }

        public Task RefreshAsync(string key, CancellationToken token = default(CancellationToken))
        {
            Refresh(key);
            return Task.CompletedTask;
        }

        public void Remove(string key)
        {
            _memoryCache.Remove(key);
        }

        public Task RemoveAsync(string key, CancellationToken token = default(CancellationToken))
        {
            _memoryCache.Remove(key);
            return Task.CompletedTask;
        }

        public void Set(string key, byte[] value, DistributedCacheEntryOptions options)
        {
            _memoryCache.Set(key, value, Map(options));
        }

        public Task SetAsync(string key, byte[] value, DistributedCacheEntryOptions options, CancellationToken token = default(CancellationToken))
        {
            _memoryCache.Set(key, value, Map(options));
            return Task.CompletedTask;
        }

        private static MemoryCacheEntryOptions Map(DistributedCacheEntryOptions source)
        {
            return new MemoryCacheEntryOptions
            {
                SlidingExpiration = source.SlidingExpiration,
                AbsoluteExpiration = source.AbsoluteExpiration,
                AbsoluteExpirationRelativeToNow = source.AbsoluteExpirationRelativeToNow
            };
        }
    }
}