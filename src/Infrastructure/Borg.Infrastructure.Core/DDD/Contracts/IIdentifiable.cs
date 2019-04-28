using System.Collections.Generic;

namespace Borg.Infrastructure.Core.DDD.Contracts
{
    public interface IIdentifiable
    {
        IEnumerable<(string key, object value)> Keys { get; }
    }
}