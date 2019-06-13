using Borg.Framework.EF;
using Borg.Framework.EF.Instructions.Attributes;
using Borg.Infrastructure.Core.Reflection.Discovery;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Borg.Platform.EF
{
    [DefaultDbDefinition]
    public class BorgPlatformDb : BorgDbContext<IConfiguration>
    {
        public BorgPlatformDb(ILoggerFactory loggerFactory, IConfiguration configuration, IAssemblyExplorerResult explorerResult) : base(loggerFactory, configuration, explorerResult)
        {
        }

        internal BorgPlatformDb(DbContextOptions options) : base(options)
        {
        }
    }
}