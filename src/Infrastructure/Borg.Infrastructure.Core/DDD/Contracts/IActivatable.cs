using System;

namespace Borg.Infrastructure.Core.DDD.Contracts
{
    public interface IActive
    {
        bool IsActive { get; set; }
    }

    public interface IActivatable : IActive
    {
        bool IsCurrentlyActive { get; set; }
        DateTimeOffset? ActiveFrom { get; set; }
        DateTimeOffset? ActiveTo { get; set; }
    }
}