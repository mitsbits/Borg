using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System.Collections.Generic;

namespace Borg.Infrastructure.Core.Reflection.Discovery
{
    public class AssemblyExplorerResult : IAssemblyExplorerResult
    {
        private ILogger Logger { get; }

        private readonly List<AssemblyScanResult> results = new List<AssemblyScanResult>();

        protected AssemblyExplorerResult(ILoggerFactory loggerFactory, IEnumerable<IAssemblyExplorer> explorers)
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