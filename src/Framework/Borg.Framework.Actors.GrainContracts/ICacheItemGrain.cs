using Orleans;
using System.Threading.Tasks;

namespace Borg.Framework.Actors.GrainContracts
{
    public interface ICacheItemGrain<T> : IGrainWithStringKey, IRemindable
    {
        Task<T> GetItem();

        Task<T> SetItem(T obj);

        Task<T> RefreshItem();

        Task RemoveItem();
    }
}