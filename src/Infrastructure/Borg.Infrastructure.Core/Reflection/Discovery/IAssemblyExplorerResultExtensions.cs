using Borg.Infrastructure.Core;
using Borg.Infrastructure.Core.Reflection.Discovery;
using System.Collections.Generic;

namespace Borg
{
    public static class IAssemblyExplorerResultExtensions
    {
        public static IEnumerable<TResult> Results<TResult>(this IAssemblyExplorerResult orchestrator) where TResult : AssemblyScanResult
        {
            orchestrator = Preconditions.NotNull(orchestrator, nameof(orchestrator));
            var results = orchestrator.Results;
            foreach (var result in results)
            {
                if (result.GetType().Equals(typeof(TResult))) yield return result as TResult;
            }
        }
    }
}