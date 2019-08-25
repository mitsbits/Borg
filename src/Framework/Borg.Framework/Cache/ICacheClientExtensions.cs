using Borg.Framework.Cache;
using Borg.Infrastructure.Core;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Borg
{
    public static class ICacheClientExtensions
    {
        public static Task Set(this ICacheClient cache, string key, object value, TimeSpan? expiresIn = null, CancellationToken cancelationToken = default)
        {
            cancelationToken.ThrowIfCancellationRequested();
            cache = Preconditions.NotNull(cache, nameof(cache));
            return cache.Set(key, value, expiresIn, cancelationToken);
        }
    }
}