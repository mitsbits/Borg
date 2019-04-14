using Borg.Framework.EF.Contracts;
using Borg.Infrastructure.Core.DI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Borg.Platform.Backoffice.Security.EF.Data
{
    [PlugableService(ImplementationOf =typeof(IDbSeed), Lifetime = Lifetime.Scoped, OneOfMany =true, Order = 1)]
    public class SecurityDbSeed : IDbSeed
    {
        private readonly SecurityDbContext _db;
        private readonly ILogger _logger;

        public SecurityDbSeed(ILoggerFactory loggerFactory, SecurityDbContext db)
        {
            _logger = loggerFactory == null ? NullLogger.Instance : loggerFactory.CreateLogger(GetType());
            _db = db;
        }

        public async Task EnsureUp()
        {
            _logger.Debug($"{nameof(SecurityDbSeed)} is about to run");
            await _db.Database.MigrateAsync();
            _logger.Debug($"{nameof(SecurityDbSeed)} run");
        }
    }
}
