using Borg.Infrastructure.Core;
using Borg.Infrastructure.Core.Reflection.Discovery;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Borg.Framework.EF.Discovery
{
    public class EntitiesExplorer : IAssemblyExplorer
    {
        private readonly ILogger logger;
        private readonly List<Assembly> assemblies = new List<Assembly>();
        private readonly List<AssemblyScanResult> results = new List<AssemblyScanResult>();

        public EntitiesExplorer(ILoggerFactory loggerfactory, IEnumerable<IAssemblyProvider> providers)
        {
            this.logger = loggerfactory == null ? NullLogger.Instance : loggerfactory.CreateLogger(GetType());
            Populate(providers);
            InternalScan();
        }

        public IEnumerable<AssemblyScanResult> Results()
        {
            return results;
        }

        private void InternalScan()
        {
            foreach (var asml in assemblies)
            {
                results.Add(InternalScan(asml));
            }
        }

        private AssemblyScanResult InternalScan(Assembly asmbl)
        {
            return AsyncHelpers.RunSync(() =>
            new AssemblyScanner.EntitiesAssemblyScanner(asmbl,
            ServiceLocator.Current.GetInstance<ILoggerFactory>())
            .Scan());
        }

        private void Populate(IEnumerable<IAssemblyProvider> providers)
        {
            providers = Preconditions.NotEmpty(providers, nameof(providers));
            foreach (var prv in providers)
            {
                var localCollection = prv.GetAssemblies();
                foreach (var asmbl in localCollection)
                {
                    if (asmbl.Assimilated() && !assemblies.Any(x => x.FullName == asmbl.FullName))
                    {
                        logger.Info($"Discoverd assembly for the hive - {asmbl.FullName}");
                        assemblies.Add(asmbl);
                    }
                }
            }
        }
    }
}