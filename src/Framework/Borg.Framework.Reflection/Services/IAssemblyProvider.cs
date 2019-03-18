using System.Collections.Generic;
using System.Reflection;

namespace Borg.Framework.Reflection.Services
{
    public interface IAssemblyProvider
    {
        IEnumerable<Assembly> GetAssemblies();
    }
}