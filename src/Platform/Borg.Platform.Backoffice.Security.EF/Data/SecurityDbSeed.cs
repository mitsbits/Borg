using Borg.Framework.EF;
using Borg.Framework.EF.Contracts;
using Borg.Infrastructure.Core.DI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Borg.Platform.Backoffice.Security.EF.Data
{
    [PlugableService(ImplementationOf = typeof(IDbSeed), Lifetime = Lifetime.Scoped, OneOfMany = true, Order = 1)]
    public class SecurityDbSeed : DbSeed<SecurityDbContext>
    {
        public SecurityDbSeed(SecurityDbContext db, ILoggerFactory loggerFactory = default(ILoggerFactory)) : base(db, loggerFactory)
        {
        }

        protected override async Task RunLocal(SecurityDbContext context, CancellationToken cancelationToken = default)
        {
            await DB.Database.MigrateAsync(cancelationToken);
        }
    }
}