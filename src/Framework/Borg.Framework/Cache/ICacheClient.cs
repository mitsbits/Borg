using System;
using System.Threading;
using System.Threading.Tasks;

namespace Borg.Framework.Cache
{
    public interface ICacheClient
    {
        //Task<int> RemoveAll(IEnumerable<string> keys = null);

        Task<T> Get<T>(string key, CancellationToken cancelationToken = default);

        //Task<IEnumerable<CacheValue<T>>> GetAll<T>(IEnumerable<string> keys);

        //Task<bool> Add<T>(string key, T value, TimeSpan? expiresIn = null);

        Task Set<T>(string key, T value, TimeSpan? expiresIn = null, CancellationToken cancelationToken = default);

        //Task<int> SetAllAsync<T>(IDictionary<string, T> values, TimeSpan? expiresIn = null);

        //Task<bool> ReplaceAsync<T>(string key, T value, TimeSpan? expiresIn = null);

        //Task<double> IncrementAsync(string key, double amount = 1, TimeSpan? expiresIn = null);

        //Task<bool> ExistsAsync(string key);

        //Task<TimeSpan?> GetExpiration(string key, CancellationToken cancelationToken = default);

        //Task SetExpirationAsync(string key, TimeSpan expiresIn);

        //Task<double> SetIfHigherAsync(string key, double value, TimeSpan? expiresIn = null);

        //Task<double> SetIfLowerAsync(string key, double value, TimeSpan? expiresIn = null);

        //Task<long> SetAddAsync<T>(string key, IEnumerable<T> value, TimeSpan? expiresIn = null);

        //Task<long> SetRemoveAsync<T>(string key, IEnumerable<T> value, TimeSpan? expiresIn = null);

        //Task<CacheValue<ICollection<T>>> GetSetAsync<T>(string key);
    }
}