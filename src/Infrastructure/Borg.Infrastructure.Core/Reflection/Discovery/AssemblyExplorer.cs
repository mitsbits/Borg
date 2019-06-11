using Borg.Infrastructure.Core.Reflection.Discovery;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System.Collections.Generic;

namespace Borg.Framework.EF.Discovery
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

        public void Scan() => ScanInternal();

        protected abstract void ScanInternal();
    }
}