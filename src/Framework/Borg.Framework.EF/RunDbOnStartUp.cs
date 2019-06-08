using Borg.Framework.EF.Contracts;
using Borg.Framework.Modularity;
using Borg.Infrastructure.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Borg.Framework.EF
{
    public abstract class RunDbOnStartUp<TDbContext> : IRunOnHostStartUp, IChainLink where TDbContext : DbContext
    {
        protected ILogger Logger { get; }
        protected TDbContext DB { get; }

        protected RunDbOnStartUp(TDbContext dbContext, ILoggerFactory loggerFactory = null)
        {
            Preconditions.NotNull(dbContext, nameof(dbContext));
            Logger = loggerFactory == null ? NullLogger.Instance : loggerFactory.CreateLogger(GetType());
            DB = dbContext;
        }

        public int Order { get; set; }

        public async Task Run(CancellationToken cancelationToken)
        {
            var watch = Stopwatch.StartNew();
            Logger.Debug($"{GetType().Name} is about to run");
            await RunLocal(DB, cancelationToken);
            watch.Stop();
            Logger.Debug($"{GetType().Name} run in {watch.Elapsed}");
        }

        protected abstract Task RunLocal(TDbContext context, CancellationToken cancelationToken = default(CancellationToken));
    }

    public abstract class DbSeed<TDbContext> : RunDbOnStartUp<TDbContext>, IDbSeed where TDbContext : DbContext
    {
        protected DbSeed(TDbContext db, ILoggerFactory loggerFactory) : base(db, loggerFactory)
        {
        }
    }

    public abstract class DbRecipe<TDbContext> : RunDbOnStartUp<TDbContext>, IDbRecipe where TDbContext : DbContext
    {
        protected DbRecipe(TDbContext db, ILoggerFactory loggerFactory) : base(db, loggerFactory)
        {
        }
    }
}