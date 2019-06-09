using Borg.Infrastructure.Core.Reflection.Discovery;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System.Collections.Generic;

namespace Borg.Framework.EF.Discovery
{
    public abstract class AssemblyExplorer : IAssemblyExplorer
    {
        protected ILogger Logger { get; }

        protected AssemblyExplorer(ILoggerFactory loggerFactory)
        {
            Logger = loggerFactory == null ? NullLogger.Instance : loggerFactory.CreateLogger(GetType());
        }

        public IEnumerable<AssemblyScanResult> Results()
        {
            return ResultsInternal();
        }

        protected abstract IEnumerable<AssemblyScanResult> ResultsInternal();
    }
}