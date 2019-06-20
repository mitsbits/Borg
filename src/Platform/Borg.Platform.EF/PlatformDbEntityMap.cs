using Borg.Platform.EF.Instructions;

namespace Borg.Platform.EF
{
    public abstract class PlatformDbEntityMap<T> : EntityMap<T, BorgDb> where T : class
    {

    }
}
