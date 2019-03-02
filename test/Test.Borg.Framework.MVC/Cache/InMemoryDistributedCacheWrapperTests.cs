using Borg.Framework.MVC.Cache;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Shouldly;
using Xunit;

namespace Test.Borg.Framework.MVC.Cache
{
    public class InMemoryDistributedCacheWrapperTests
    {
      
        private readonly IDistributedCache _cache;

        public InMemoryDistributedCacheWrapperTests()
        {
          var   memoryCache = new MemoryCache(new MemoryCacheOptions());
            _cache = new InMemoryDistributedCacheWrapper(memoryCache);
        }

        [Fact]
        public void set_and_get()
        {
            var testdata = new TestData();
            var key = "key";
            _cache.Set(key, testdata._objData);
            var value = _cache.Get(key);
            value.ShouldBe(testdata._objData);
    }
    }

}