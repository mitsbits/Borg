using Borg.Framework.Modularity;
using System.Threading.Tasks;

namespace Borg.Framework.EF.Contracts
{
    public interface IDbSeed : IRunOnHostStartUp, IChainLink
    {

    }
}