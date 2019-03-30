using System.Collections.Generic;

namespace Borg.Framework.DAL
{
    public interface ICanProduceOrderBys<T> where T : class
    {
        IEnumerable<OrderByInfo<T>> Build();
    }
}