using Borg.Infrastructure.Core.DDD.ValueObjects;

namespace Borg.Infrastructure.Core.DDD.Contracts
{
    public interface IHaveCompositeKey
    {
        CompositeKey CompositeKey { get; }
    }
}