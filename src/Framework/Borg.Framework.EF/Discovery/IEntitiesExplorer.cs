using Borg.Infrastructure.Core;
using System.Collections.Generic;

namespace Borg.Framework.EF.Discovery
{
    public interface IEntitiesExplorer
    {
        IEnumerable<AssemblyScanResult> Results();
    }
}