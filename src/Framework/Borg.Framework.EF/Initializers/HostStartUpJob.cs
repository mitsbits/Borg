using Borg.Framework.Modularity.Pipelines;
using Borg.Infrastructure.Core;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Borg.Framework.EF.Initializers
{
    public class HostStartUpJo<TDbContext> : GenericHostStartUp where TDbContext : BorgDbContext
    {
        public HostStartUpJo()
        {

        }
    }
    public abstract class MigrationStep<TDbContext> : GenericPipelineStep<HostStartUpJo<TDbContext>> where TDbContext : BorgDbContext
    {
        protected ILogger Logger { get; }
        protected TDbContext DB { get; }


        public MigrationStep(ILoggerFactory loggerFactory)
        {

        }
        public abstract Task Execute(CancellationToken cancelationToken);
    }

    public abstract class BorgDbPipelineStep<TDbContext>  where TDbContext : BorgDbContext
    {
        protected ILogger Logger { get; }
        protected TDbContext DB { get; }
        public BorgDbPipelineStep(TDbContext dbContext, ILoggerFactory loggerFactory = null)
        {
            DB = Preconditions.NotNull(dbContext, nameof(dbContext));
            Logger = loggerFactory == null ? NullLogger.Instance : loggerFactory.CreateLogger(GetType());
        }

    }
}
