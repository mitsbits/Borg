using Borg.Infrastructure.Core.DDD.ValueObjects;
using System;

namespace Borg.Infrastructure.Core.DDD.Contracts
{
    public interface IHaveCompositeKey 
    {
        CompositeKey CompositeKey { get; }
    }

}