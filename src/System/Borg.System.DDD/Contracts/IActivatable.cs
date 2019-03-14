using System;

namespace Borg.System.DDD.Contracts
{
    public interface IActivatable
    {
        bool IsActive { get; set; }
        bool IsCurrentlyActive { get; set; }
        DateTimeOffset? ActiveFrom { get; set; }
        DateTimeOffset? ActiveTo { get; set; }
    }
}