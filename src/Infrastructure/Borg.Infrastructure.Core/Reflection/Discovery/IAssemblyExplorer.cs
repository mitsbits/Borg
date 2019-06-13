using System.Collections.Generic;

namespace Borg.Infrastructure.Core.Reflection.Discovery
{
    public interface IAssemblyExplorer
    {
        IEnumerable<AssemblyScanResult> Results();

        bool ScanCompleted { get; }

        void Scan();
    }
}

namespace Borg
{
}