using Borg.Framework.EF.Contracts;
using Borg.Infrastructure.Core.DI;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Threading.Tasks;

namespace Borg.Platform.Backoffice.Security.EF.Data
{
    [PlugableService(ImplementationOf = typeof(IDbRecipe), Lifetime = Lifetime.Scoped, OneOfMany = true, Order = 1)]
    public class SecurityDbRecipe : IDbRecipe
    {
        private readonly SecurityDbContext _db;
        private readonly ILogger _logger;

        public SecurityDbRecipe(ILoggerFactory loggerFactory, SecurityDbContext db)
        {
            _logger = loggerFactory == null ? NullLogger.Instance : loggerFactory.CreateLogger(GetType());
            _db = db;
        }

        public Task Populate()
        {
            throw new NotImplementedException();
        }
    }
}