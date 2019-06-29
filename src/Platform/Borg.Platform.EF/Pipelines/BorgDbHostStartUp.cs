using Borg.Framework.EF.Initializers;
using Borg.Framework.Modularity.Pipelines;
using Borg.Infrastructure.Core.DI;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;


namespace Borg.Platform.EF.Pipelines
{
    [PlugableService(ImplementationOf = typeof(IHostStartUpJob), Lifetime = Lifetime.Scoped, OneOfMany = true)]
    public class BorgDbHostStartUp : HostStartUpJob<BorgDb>
    {
     
        public BorgDbHostStartUp(BorgDb dbContext, IEnumerable<IPipelineStep<BorgDbHostStartUp>> steps, ILoggerFactory loggerFactory = null) : base(dbContext, steps, loggerFactory)
        {
        }
    }
    [PlugableService(ImplementationOf = typeof(IPipelineStep<BorgDbHostStartUp>), Lifetime = Lifetime.Scoped, OneOfMany = true)]
    public class BorgDbEnsureUp : EnsuseUp<BorgDb, BorgDbHostStartUp>
    {
        public BorgDbEnsureUp(BorgDb db, ILoggerFactory loggerFactory):base(db, loggerFactory)
        {

        }
        public override double Weight { get => 0; set => base.Weight = 0; }
    }
}
