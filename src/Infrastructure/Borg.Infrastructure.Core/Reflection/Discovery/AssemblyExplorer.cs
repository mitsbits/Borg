using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Borg.Infrastructure.Core.Reflection.Discovery
{
    public abstract class AssemblyExplorer : IAssemblyExplorer
    {
        protected ILogger Logger { get; }
        protected bool scanCompleted = false;

        protected AssemblyExplorer(ILoggerFactory loggerFactory)
        {
            Logger = loggerFactory == null ? NullLogger.Instance : loggerFactory.CreateLogger(GetType());
        }

        public IEnumerable<AssemblyScanResult> Results()
        {
            return ResultsInternal();
        }

        public bool ScanCompleted => scanCompleted;

        protected abstract IEnumerable<AssemblyScanResult> ResultsInternal();

        public Task Scan() => ScanInternal();

        protected abstract Task ScanInternal();
    }
}