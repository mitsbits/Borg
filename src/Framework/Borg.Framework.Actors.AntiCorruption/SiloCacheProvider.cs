using Borg.Framework.Actors.GrainContracts;
using Borg.Infrastructure.Core.Threading;
using Microsoft.Extensions.Caching.Distributed;
using Orleans;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Borg.Framework.Actors.AntiCorruption
{

    public class SiloCacheProvider : IDistributedCache
    {

        private readonly IClusterClient _clusterClient;
        public SiloCacheProvider(IClusterClient clusterClient)
        {
            _clusterClient = clusterClient;
        }
        public byte[] Get(string key)
        {
            return AsyncHelpers.RunSync(() => GetAsync(key));
        }

        public async Task<byte[]> GetAsync(string key, CancellationToken token = default)
        {
            token.ThrowIfCancellationRequested();
            var grain = _clusterClient.GetGrain<ICacheItemGrain>(key);
            var state = await grain.GetItem();
            if (state == null)
            {
                return new byte[0];
            }
            return state.Data;
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

        public async Task RemoveAsync(string key, CancellationToken token = default)
        {
            token.ThrowIfCancellationRequested();
            var grain = _clusterClient.GetGrain<ICacheItemGrain>(key);
            await grain.SetItem(null);
        }

        public void Set(string key, byte[] value, DistributedCacheEntryOptions options)
        {
            AsyncHelpers.RunSync(() => SetAsync(key, value, options));
        }

        public async Task SetAsync(string key, byte[] value, DistributedCacheEntryOptions options, CancellationToken token = default)
        {
            token.ThrowIfCancellationRequested();
            var grain = _clusterClient.GetGrain<ICacheItemGrain>(key);
            await grain.SetItem(new CacheItemState()
            {
                Data = value,
                AbsoluteExpiration = options.AbsoluteExpiration,
                AbsoluteExpirationRelativeToNow = options.AbsoluteExpirationRelativeToNow,
                SlidingExpiration = options.SlidingExpiration });
        }
    }
}
