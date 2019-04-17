using System.Collections.Generic;

namespace Borg.Framework.Modularity
{
    public interface IChain
    {
        IEnumerable<IChainLink> ChainLinks { get; }
    }
}
