using Borg.Infrastructure.Core.DDD.ValueObjects;
using System.Linq;

namespace Borg.Infrastructure.Core.DDD.Contracts
{
    public interface IIdentifiable
    {
        CompositeKey Keys { get; }
    }
}