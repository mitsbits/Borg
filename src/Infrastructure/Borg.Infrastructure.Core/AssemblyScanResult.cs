using System;
using System.Collections.Generic;
using System.Reflection;

namespace Borg.Infrastructure.Core
{
    public struct AssemblyScanResult
    {
        public AssemblyScanResult(Assembly assembly, List<Type> aggregateRoots, Dictionary<Type, Type[]> complexTypes)
        {
            Assembly = Preconditions.NotNull(assembly, nameof(assembly));
            AggregateRoots = Preconditions.NotEmpty(aggregateRoots, nameof(aggregateRoots));
            ComplexTypes = Preconditions.NotEmpty(complexTypes, nameof(complexTypes));
            Success = true;
            Errors = new string[0];
        }

        public AssemblyScanResult(Assembly assembly, string[] errors)
        {
            Assembly = Preconditions.NotNull(assembly, nameof(assembly));
            AggregateRoots = null;
            ComplexTypes = null;
            Success = false;
            Errors = errors;
        }

        public bool Success { get; }
        public Assembly Assembly { get; }
        IEnumerable<Type> AggregateRoots { get; }
        IEnumerable<KeyValuePair<Type, Type[]>> ComplexTypes { get; }
        public string[] Errors { get; }
    }
}