using Borg.Infrastructure.Core.DDD.Contracts;
using System.Collections.Generic;

namespace Borg.System.AddOn
{
    public interface IAddOn : IHaveTitle
    {
        bool IsSystem { get; }
        ICollection<IFeature> Features { get; }
    }

    public interface IFeature : IHaveTitle
    {
    }

    public abstract class AddOn : IAddOn
    {
        public bool IsSystem { get; }
        public string Title { get; }

        public abstract ICollection<IFeature> Features { get; }
    }
}