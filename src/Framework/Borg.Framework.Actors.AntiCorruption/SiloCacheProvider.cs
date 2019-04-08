using Borg.Infrastructure.Core.Threading;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Borg.Framework.Actors.AntiCorruption
{
    public class SiloCacheProvider : IDistributedCache
    {
        public byte[] Get(string key)
        {
            return AsyncHelpers.RunSync(() => GetAsync(key));
        }

        public Task<byte[]> GetAsync(string key, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        public void Refresh(string key)
        {
            AsyncHelpers.RunSync(() => RefreshAsync(key));
        }

        public Task RefreshAsync(string key, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        public void Remove(string key)
        {
            AsyncHelpers.RunSync(() => RemoveAsync(key));
        }

        public Task RemoveAsync(string key, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        public void Set(string key, byte[] value, DistributedCacheEntryOptions options)
        {
            AsyncHelpers.RunSync(() => SetAsync(key, value, options));
        }

        public Task SetAsync(string key, byte[] value, DistributedCacheEntryOptions options, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }
    }
}
