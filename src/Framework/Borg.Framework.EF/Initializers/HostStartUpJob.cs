using Borg.Framework.Modularity.Pipelines;
using Borg.Infrastructure.Core.DI;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace Borg.Framework.EF.Initializers
{
    [PlugableService(ImplementationOf = typeof(IHostStartUpJob), Lifetime = Lifetime.Scoped, OneOfMany = true)]
    public class HostStartUpJob<TDbContext> : GenericHostStartUp where TDbContext : BorgDbContext
    {
        public HostStartUpJob(TDbContext dbContext, IEnumerable<IPipelineStep<HostStartUpJob<TDbContext>>> steps, ILoggerFactory loggerFactory = null)
        {
        }
    }
}