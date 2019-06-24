using Borg.Infrastructure.Core.DDD.ValueObjects;
using Borg.Infrastructure.Core.Reflection.Discovery.Annotations;

namespace Borg.Infrastructure.Core.DDD.Contracts
{
    public interface IHaveCompositeKey
    {
        CompositeKey CompositeKey { get; }
    }
}