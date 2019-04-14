using Borg.Framework.EF.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Borg.Platform.Backoffice.Security.EF.Data
{
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
            await _db.Database.MigrateAsync();
        }
    }
}
