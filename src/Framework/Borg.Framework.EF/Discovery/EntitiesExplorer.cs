﻿using Borg.Infrastructure.Core;
using Borg.Infrastructure.Core.Reflection.Discovery;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Borg.Framework.EF.Discovery
{
    public class EntitiesExplorer : AssemblyExplorer
    {
        private readonly List<Assembly> assemblies = new List<Assembly>();
        private readonly List<AssemblyScanResult> results = new List<AssemblyScanResult>();

        public EntitiesExplorer(ILoggerFactory loggerfactory, IEnumerable<IAssemblyProvider> providers) : base(loggerfactory)
        {
            Populate(Preconditions.NotEmpty(providers, nameof(providers)));
            ScanInternal();
        }

        protected override IEnumerable<AssemblyScanResult> ResultsInternal()
        {
            return results;
        }

        protected override void ScanInternal()
        {
            foreach (var asml in assemblies)
            {
                results.Add(ScanInternal(asml));
            }
            scanCompleted = true;
        }

        private AssemblyScanResult ScanInternal(Assembly asmbl)
        {
            return AsyncHelpers.RunSync(() =>
            new AssemblyScanner.EntitiesAssemblyScanner(asmbl,
            ServiceLocator.Current.GetInstance<ILoggerFactory>())
            .Scan());
        }

        private void Populate(IEnumerable<IAssemblyProvider> providers)
        {
            foreach (var asmbl in providers.SelectMany(p => p.GetAssemblies())
                .Where(asmbl => asmbl.Assimilated()
                    && !assemblies.Any(x => x.FullName == asmbl.FullName)))
            {
                Logger.Info($"Discoverd assembly for the hive - {asmbl.FullName}");
                assemblies.Add(asmbl);
            }
        }
    }
}