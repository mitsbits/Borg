﻿using Borg.Framework.Actors.GrainContracts;
using Orleans;
using Orleans.Providers;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Borg.Framework.Actors.Grains
{


    public class StateHolder<T>
    {
        public StateHolder() : this(default(T))
        {
        }

        public StateHolder(T value)
        {
            Value = value;
        }

        public T Value { get; set; }
    }

    public abstract class StateHolderGrain<T> : Grain<StateHolder<T>>, IStateHolderGrain<T>
    {
        public virtual Task<T> GetItem()
        {
            return Task.FromResult(State.Value);
        }

        public virtual async Task<T> SetItem(T item)
        {
            State.Value = item;
            await WriteStateAsync();

            return State.Value;
        }
    }
    [StorageProvider(ProviderName = "Actors")]
    public class CacheItemGrain : Grain<CacheItemState>, ICacheItemGrain
    {
    
        public async Task<CacheItemState> GetItem()
        {
            if (State.AbsoluteExpiration.HasValue && State.AbsoluteExpiration.Value < DateTimeOffset.UtcNow)
            {
                await ClearStateAsync();
                return default(CacheItemState);
            }
            return State;
        }

        public async Task<CacheItemState> SetItem(CacheItemState obj)
        {
            if (obj.AbsoluteExpiration.HasValue && obj.AbsoluteExpiration.Value < DateTimeOffset.UtcNow)
            {
                await ClearStateAsync();
                return default(CacheItemState);
            }

            State = obj;
            await WriteStateAsync();
            return State;
        }
    }
}
