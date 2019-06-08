using Borg.Infrastructure.Core.Reflection.Discovery;
using System.Collections.Generic;

namespace Borg.Framework.EF.Discovery
{
    public interface IAssemblyExplorer
    {
        IEnumerable<AssemblyScanResult> Results();
    }
}