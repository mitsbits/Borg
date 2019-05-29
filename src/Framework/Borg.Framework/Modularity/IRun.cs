using System.Threading;
using System.Threading.Tasks;

namespace Borg.Framework.Modularity
{
    public interface IRun
    {
        Task Run(CancellationToken cancelationToken);
    }
}
