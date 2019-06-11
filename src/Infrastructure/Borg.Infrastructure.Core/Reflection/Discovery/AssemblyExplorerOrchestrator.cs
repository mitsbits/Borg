using Borg.Infrastructure.Core;
using Borg.Infrastructure.Core.Reflection.Discovery;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System.Collections.Generic;

namespace Borg.Framework.EF.Discovery
{
    public class AssemblyExplorerOrchestrator : IAssemblyExplorerOrchestrator
    {
        private ILogger Logger { get; }

        private readonly List<AssemblyScanResult> results = new List<AssemblyScanResult>();

        protected AssemblyExplorerOrchestrator(ILoggerFactory loggerFactory, IEnumerable<IAssemblyExplorer> explorers)
        {
            Logger = loggerFactory == null ? NullLogger.Instance : loggerFactory.CreateLogger(GetType());
            Populate(Preconditions.NotEmpty(explorers, nameof(explorers)));
        }

        private void Populate(IEnumerable<IAssemblyExplorer> explorers)
        {
            foreach (var explorer in explorers)
            {
                if (!explorer.ScanCompleted) explorer.Scan();
                if (explorer.ScanCompleted)
                {
                    results.AddRange(explorer.Results());
                }
            }
        }

        public IEnumerable<AssemblyScanResult> Results => throw new System.NotImplementedException();
    }
}