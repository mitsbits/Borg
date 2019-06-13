using Borg.Framework.EF;
using Borg.Infrastructure.Core.Reflection.Discovery;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Borg.Platform.Backoffice.Security.EF.Data
{
    public class SecurityDbContext : BorgDbContext<IConfiguration>
    {
        public SecurityDbContext(ILoggerFactory loggerFactory, IConfiguration configuration, IAssemblyExplorerResult explorerResult) : base(loggerFactory, configuration, explorerResult)
        {
        }
    }
}