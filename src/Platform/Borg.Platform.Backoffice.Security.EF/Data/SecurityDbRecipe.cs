using Borg.Framework.EF;
using Borg.Framework.EF.Contracts;

using Borg.Infrastructure.Core.DI;
using Borg.Infrastructure.Core.Reflection.Discovery;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Borg.Platform.Backoffice.Security.EF.Data
{
    [PlugableService(ImplementationOf = typeof(IDbRecipe), Lifetime = Lifetime.Scoped, OneOfMany = true, Order = 1)]
    public class SecurityDbRecipe : DbRecipe<SecurityDbContext>
    {
        public SecurityDbRecipe(ILoggerFactory loggerFactory, SecurityDbContext db, IEnumerable<IAssemblyProvider> assemblyProviders) : base(db, loggerFactory)
        {
        }

        protected override Task RunLocal(SecurityDbContext context, CancellationToken cancelationToken = default)
        {
            return Task.CompletedTask;
        }
    }
}