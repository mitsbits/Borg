using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Borg.Framework.Modularity
{
    public interface IRunOnHostStartUp
    {
        Task Run(CancellationToken cancelationToken );
    }
}
