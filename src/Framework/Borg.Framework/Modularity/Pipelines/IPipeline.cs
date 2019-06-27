using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Borg.Framework.Modularity.Pipelines
{
   public interface IHostStartUpJob : IPipeline, IExecutor
    {

    }
    public interface IPipeline : IEnumerable<IPipelineStep<IPipeline>>
    {
    }

    public interface IPipelineStep<out TPipeline> : IExecutor where TPipeline : IPipeline
    {
    }


    public interface IExecutor
    {
        Task Execute(CancellationToken cancelationToken);
    }
}
