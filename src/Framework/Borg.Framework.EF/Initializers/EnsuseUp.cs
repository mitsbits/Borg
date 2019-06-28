using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Borg.Framework.EF.Initializers
{
    public class EnsuseUp<TDbContext> : BorgDbPipelineStep<TDbContext> where TDbContext : BorgDbContext
    {
        protected EnsuseUp(TDbContext db, ILoggerFactory loggerFactory) : base(db, loggerFactory)
        {
        }

        public override double Weight { get => 0; set => throw new NotImplementedException(nameof(Weight)); }

        protected override async Task ExecuteInternal(CancellationToken cancelationToken)
        {
            Logger.Debug($"About to migrate {DB.Schema}");
            await DB.Database.MigrateAsync(cancelationToken);
        }
    }
}