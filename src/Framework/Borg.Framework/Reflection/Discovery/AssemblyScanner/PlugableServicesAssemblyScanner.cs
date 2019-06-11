using Borg.Infrastructure.Core.Reflection.Discovery;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Borg.Framework.Reflection.Discovery.AssemblyScanner
{
    public class PlugableServicesAssemblyScanner : AssemblyScanner<PlugableServicesAssemblyScanResult>
    {
        private List<PlugableServicesAssemblyScanResult.Instruction> instructions = new List<PlugableServicesAssemblyScanResult.Instruction>();
        private PlugableServicesAssemblyScanResult Result;

        public PlugableServicesAssemblyScanner(Assembly assembly, ILoggerFactory loggerfactory) : base(loggerfactory, assembly)
        {
            Populate();
        }

        private void Populate()
        {
            var elligibleTypes = Assembly.GetTypes().Where(x => x.IsPlugableService());
            if (!elligibleTypes.Any())
            {
                Result = new PlugableServicesAssemblyScanResult(Assembly, new string[] { new NoPlugableServicesException(Assembly).ToString() });
                return;
            }
        }

        protected override Task<PlugableServicesAssemblyScanResult> ScanInternal()
        {
            return Task.FromResult(Result);
        }
    }
}