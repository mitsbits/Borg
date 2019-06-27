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

        public Task Execute(CancellationToken cancelationToken)
        {
            throw new NotImplementedException();
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
        public abstract Task Execute(CancellationToken cancelationToken);
    }
}