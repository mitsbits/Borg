using Borg.Framework.EF;
using Borg.Framework.EF.Instructions.Attributes;
using Borg.Infrastructure.Core.Reflection.Discovery;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Borg.Platform.EF
{
    [DefaultDbDefinition]
    public class BorgDb : BorgDbContext<IConfiguration>
    {
        public BorgDb(ILoggerFactory loggerFactory, IConfiguration configuration, IAssemblyExplorerResult explorerResult) : base(loggerFactory, configuration, explorerResult)
        {
        }
        internal BorgDb(DbContextOptions options, IAssemblyExplorerResult explorerResult) : base(options, explorerResult)
        {
        }
        internal BorgDb(DbContextOptions options) : base(options)
        {
        }
    }
}