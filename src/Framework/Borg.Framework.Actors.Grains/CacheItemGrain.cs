using Borg.Framework.Actors.GrainContracts;
using Orleans;
using Orleans.Providers;
using Orleans.Runtime;
using System;
using System.Threading.Tasks;
namespace Borg.Framework.Actors.Grains
{
    [StorageProvider(ProviderName = "ef")]
    public class CacheItemGrain : Grain<CacheItemState<byte[]>>, ICacheItemGrain<CacheItemState<byte[]>>
    {
        public CacheItemGrain()
        {
        }

        private string InvalidationReminderName { get => "Reminder" + IdentityString; }
        private IGrainReminder Reminder { get; set; }

        public Task<CacheItemState<byte[]>> GetItem()
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

        public async Task<CacheItemState<byte[]>> RefreshItem()
        {
            await ReadStateAsync();
            return await GetItem();
        }

        public async Task RemoveItem()
        {
            await ClearStateAsync();
            await UnregisterReminder(Reminder);
        }

        public async Task<CacheItemState<byte[]>> SetItem(CacheItemState<byte[]> obj)
        {
            State = obj;

            if (obj.Expiration.HasValue && obj.Expiration.Value.ToUniversalTime() < DateTimeOffset.UtcNow.AddDays(50))
            {
                if (obj.Expiration.Value.ToUniversalTime() < DateTimeOffset.UtcNow) throw new InvalidOperationException("can not se expiration in the past");
                Reminder = await RegisterOrUpdateReminder(InvalidationReminderName, obj.Expiration.Value.Subtract(DateTimeOffset.UtcNow), TimeSpan.FromDays(1));
            }
            await WriteStateAsync();

            return State;
        }
    }
}