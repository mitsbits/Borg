using Borg.Infrastructure.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Borg.Framework.EF.Initializers
{
    public class ExecuteSql<TDbContext> : BorgDbPipelineStep<TDbContext> where TDbContext : BorgDbContext
    {
        protected string SqlText;
        private readonly string command;
        private readonly object[] parameters;

        protected ExecuteSql(TDbContext db, ILoggerFactory loggerFactory, string command, params object[] parameters) : base(db, loggerFactory)
        {
            SqlText = Preconditions.NotEmpty(command, nameof(command));
            this.command = Preconditions.NotEmpty(command, nameof(command));
            db.Database.ExecuteSqlCommandAsync(SqlText, default, parameters);
        }

        public override double Weight { get; set; } = 0;

        protected override async Task ExecuteInternal(CancellationToken cancelationToken)
        {
            cancelationToken.ThrowIfCancellationRequested();
            try
            {
                Logger.Debug($"About to Execute SQL");
                await DB.Database.ExecuteSqlCommandAsync(command, cancelationToken, parameters);
            }
            catch (Exception ex)
            {
                Logger.Error($"Unable to execute comamand - reason : {ex}");
                throw;
            }
        }
    }
}