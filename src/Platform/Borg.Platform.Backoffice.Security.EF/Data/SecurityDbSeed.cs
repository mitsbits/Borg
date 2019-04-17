using Borg.Framework.EF;
using Borg.Framework.EF.Contracts;
using Borg.Infrastructure.Core.DI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System.Threading;
using System.Threading.Tasks;

namespace Borg.Platform.Backoffice.Security.EF.Data
{
    [PlugableService(ImplementationOf = typeof(IDbSeed), Lifetime = Lifetime.Scoped, OneOfMany = true, Order = 1)]
    public class SecurityDbSeed : DbSeed<SecurityDbContext>
    {


        public SecurityDbSeed(SecurityDbContext db , ILoggerFactory loggerFactory = default(ILoggerFactory) ) :base(db, loggerFactory)
        {

        }

    }
}