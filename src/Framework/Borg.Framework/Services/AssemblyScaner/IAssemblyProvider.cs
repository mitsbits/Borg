using System.Collections.Generic;
using System.Reflection;

namespace Borg.Framework.Services.AssemblyScaner
{
    public interface IAssemblyProvider
    {
        IEnumerable<Assembly> GetAssemblies();
    }
}