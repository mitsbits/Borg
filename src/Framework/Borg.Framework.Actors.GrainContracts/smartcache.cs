using Orleans;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Borg.Framework.Actors.GrainContracts
{
    public interface IStateHolderGrain<T> : IGrainWithGuidKey
    {
        Task<T> GetItem();
        Task<T> SetItem(T obj);
    }

    public interface ICacheItemGrain : IGrainWithStringKey 
    {
        Task<CacheItemState> GetItem();
        Task<CacheItemState> SetItem(CacheItemState obj);
    }

    public class CacheItemState : ICacheItemState
    {
        public DateTimeOffset? AbsoluteExpiration { get; set; }
        public TimeSpan? AbsoluteExpirationRelativeToNow { get; set; }
        public TimeSpan? SlidingExpiration { get; set; }
        public byte[] Data { get; set; }
    }

    public interface ICacheItemState
    {


        DateTimeOffset? AbsoluteExpiration { get; set; }

        TimeSpan? AbsoluteExpirationRelativeToNow { get; set; }

        TimeSpan? SlidingExpiration { get; set; }

        byte[] Data { get; set; }
    }
}
