using System;
using System.Collections.Generic;
using System.Reflection;

namespace Borg.Infrastructure.Core.Reflection.Discovery
{
    public class EntitiesAssemblyScanResult : AssemblyScanResult
    {
        public EntitiesAssemblyScanResult(Assembly assembly, List<Type> aggregateRoots, Dictionary<Type, Type[]> complexTypes, IEnumerable<Type> defaultDbs = null, List<Type> entityMaps = null) : base(assembly, true, new string[0])
        {
            AggregateRoots = Preconditions.NotEmpty(aggregateRoots, nameof(aggregateRoots));
            ComplexTypes = Preconditions.NotEmpty(complexTypes, nameof(complexTypes));
            EntityMaps = entityMaps;
            DefaultDbs = defaultDbs;
        }

        public EntitiesAssemblyScanResult(Assembly assembly, string[] errors) : base(assembly, false, errors)
        {
            AggregateRoots = null;
            ComplexTypes = null;
            EntityMaps = null;
            DefaultDbs = null;
        }

        public IEnumerable<Type> AggregateRoots { get; }
        public IEnumerable<KeyValuePair<Type, Type[]>> ComplexTypes { get; }
        public IEnumerable<Type> EntityMaps { get; }
        public IEnumerable<Type> DefaultDbs { get; }
    }
}