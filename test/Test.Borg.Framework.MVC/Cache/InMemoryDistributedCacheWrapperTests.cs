using Borg.Framework.MVC.Cache;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;

namespace Test.Borg.Framework.MVC.Cache
{
    public class InMemoryDistributedCacheWrapperTests
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IDistributedCache _cache;

        public InMemoryDistributedCacheWrapperTests()
        {
            _memoryCache = new MemoryCache(new MemoryCacheOptions());
            _cache = new InMemoryDistributedCacheWrapper(_memoryCache);
        }
    }
}