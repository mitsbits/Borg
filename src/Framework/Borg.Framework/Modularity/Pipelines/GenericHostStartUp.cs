using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Borg.Framework.Modularity.Pipelines
{
    public abstract class GenericHostStartUp : IHostStartUpJob
    {
        private readonly ICollection<GenericPipelineStep<GenericHostStartUp>> source = new HashSet<GenericPipelineStep<GenericHostStartUp>>();

        public async Task Execute(CancellationToken cancelationToken)
        {
            cancelationToken.ThrowIfCancellationRequested();
            foreach (var step in this.LightToHeavy())
            {
                await step.Execute(cancelationToken);
            }
        }

        public IEnumerator<IPipelineStep<IPipeline>> GetEnumerator()
        {
            return source.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public abstract class GenericPipelineStep<TPipeline> : IPipelineStep<TPipeline> where TPipeline : IPipeline
    {
        public virtual double Weight { get; set; } = 0;

        public abstract Task Execute(CancellationToken cancelationToken);
    }
}