using Borg.Framework.Actors.GrainContracts;
using Orleans;
using Orleans.Providers;
using Orleans.Runtime;
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
    [StorageProvider(ProviderName = "TableStore")]
    public class CacheItemGrain : Grain<CacheItemState>, ICacheItemGrain
    {
        private string InvalidationReminderName { get => "Reminder" + IdentityString; }
        private IGrainReminder Reminder { get; set; }
        public Task<CacheItemState> GetItem()
        {
            return Task.FromResult(State);
        }

        public async Task ReceiveReminder(string reminderName, TickStatus status)
        {
            if (reminderName == InvalidationReminderName)
            {
                if (status.FirstTickTime.ToUniversalTime() < DateTimeOffset.UtcNow)
                {
                    await RemoveItem();

                }
            }
        }

        public async Task<CacheItemState> RefreshItem()
        {
            await ReadStateAsync();
            return await GetItem();
        }

        public async Task RemoveItem()
        {
            await this.ClearStateAsync();
            await UnregisterReminder(Reminder);
        }

        public async Task<CacheItemState> SetItem(CacheItemState obj)
        {
            var expiration = obj.DetermineExpiration();
            if (expiration < DateTimeOffset.UtcNow) throw new Exception("invalid expiration dude");
            obj.AbsoluteExpiration = expiration;
            State = obj;
            await WriteStateAsync();
            Reminder = await RegisterOrUpdateReminder(InvalidationReminderName, expiration.Value.Subtract(DateTimeOffset.UtcNow), TimeSpan.FromDays(1));
            return State;
        }
    }

    internal static class CacheItemStateExtensions
    {
        public static DateTimeOffset? DetermineExpiration(this ICacheItemState state)
        {
            if (state.AbsoluteExpiration.HasValue) return state.AbsoluteExpiration.Value;
            if (state.AbsoluteExpirationRelativeToNow.HasValue) return DateTimeOffset.UtcNow.Add(state.AbsoluteExpirationRelativeToNow.Value);
            if (state.SlidingExpiration.HasValue) return DateTimeOffset.UtcNow.Add(state.AbsoluteExpirationRelativeToNow.Value);
            return DateTimeOffset.UtcNow.AddDays(360);
        }
    }
}
