using Borg.Framework.EF.Contracts;
using Borg.Infrastructure.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System.Threading;
using System.Threading.Tasks;

namespace Borg.Framework.EF
{
    public abstract class DbSeed<TDbContext> : IDbSeed where TDbContext : DbContext
    {
        protected ILogger Logger { get; }
        protected TDbContext DB { get; }

        protected DbSeed(TDbContext dbContext, ILoggerFactory loggerFactory = null)
        {
            Preconditions.NotNull(dbContext, nameof(dbContext));
            DB = dbContext;
            Logger = loggerFactory == null ? NullLogger.Instance : loggerFactory.CreateLogger(GetType());
        }

        public virtual int Order { get; set; }

        public virtual async Task Run(CancellationToken cancelationToken = default(CancellationToken))
        {
            Logger.Debug($"{nameof(IDbSeed)}:{GetType().Name} is about to run");
            await DB.Database.MigrateAsync(cancelationToken);
            Logger.Debug($"{nameof(IDbSeed)}:{GetType().Name} run");
        }
    }
}