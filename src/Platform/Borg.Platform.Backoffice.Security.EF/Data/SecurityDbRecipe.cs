using Borg.Framework.EF.Contracts;
using Borg.Framework.Services.AssemblyScanner;
using Borg.Infrastructure.Core.DI;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Borg.Platform.Backoffice.Security.EF.Data
{
    [PlugableService(ImplementationOf = typeof(IDbRecipe), Lifetime = Lifetime.Scoped, OneOfMany = true, Order = 1)]
    public class SecurityDbRecipe : IDbRecipe
    {
        private readonly SecurityDbContext _db;
        private readonly ILogger _logger;
        private readonly IEnumerable<IAssemblyProvider> assemblyProviders;

        public SecurityDbRecipe(ILoggerFactory loggerFactory, SecurityDbContext db, IEnumerable<IAssemblyProvider> assemblyProviders)
        {
            _logger = loggerFactory == null ? NullLogger.Instance : loggerFactory.CreateLogger(GetType());
            _db = db;
            this.assemblyProviders = assemblyProviders;
        }

        public Task Populate()
        {
            return Task.CompletedTask;
        }

     
    }
}