using Borg.Framework.EF;
using Borg.Framework.EF.Contracts;
using Borg.Infrastructure.Core.DI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Borg.Platform.EF
{
    [PlugableService(ImplementationOf = typeof(IDbSeed), Lifetime = Lifetime.Scoped, OneOfMany = true, Order = 1)]
    public class BorgPlatformDbSeed : DbSeed<BorgDb>
    {
        public BorgPlatformDbSeed(BorgDb db, ILoggerFactory loggerFactory) : base(db, loggerFactory)
        {
        }

        protected override async Task RunLocal(BorgDb context, CancellationToken cancelationToken = default)
        {
            await context.Database.MigrateAsync(cancelationToken);
        }
    }
}