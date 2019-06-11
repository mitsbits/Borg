using Borg.Infrastructure.Core.Reflection.Discovery;
using System.Collections.Generic;

namespace Borg.Framework.EF.Discovery
{
    public interface IAssemblyExplorerOrchestrator
    {
        IEnumerable<AssemblyScanResult> Results { get; }
    }
}