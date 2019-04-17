using System.Collections.Generic;
using System.Reflection;

namespace Borg.Framework.Services.AssemblyScanner
{
    public interface IAssemblyProvider
    {
        IEnumerable<Assembly> GetAssemblies();
    }
}