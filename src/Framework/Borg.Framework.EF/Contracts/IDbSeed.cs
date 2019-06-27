using Borg.Framework.Modularity;

namespace Borg.Framework.EF.Contracts
{
    public interface IDbSeed : IRunOnHostStartUp, IPipelineStep
    {
    }
}