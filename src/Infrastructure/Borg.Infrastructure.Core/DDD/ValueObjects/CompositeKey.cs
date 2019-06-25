using Borg.Infrastructure.Core.Reflection.Discovery.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using static Borg.Infrastructure.Core.DDD.ValueObjects.CompositeKeyBuilder;

namespace Borg.Infrastructure.Core.DDD.ValueObjects
{
    [Serializable]
    [MapperIgnore]
    public class CompositeKey : ValueObject<CompositeKey>
    {
        protected readonly List<(string key, object value)> _data;

        public CompositeKey(IEnumerable<KeyValuePair<string, object>> source) : this(source.Select(x => (key: x.Key, value: x.Value)))
        {
        }

        public CompositeKey(IEnumerable<(string key, object value)> source) : this()
        {
            source = Preconditions.NotEmpty(source, nameof(source));
            foreach (var incoming in source)
            {
                Add(incoming.key, incoming.value);
            }
        }

        public CompositeKey()
        {
            _data = new List<(string key, object value)>();
        }

        public virtual void Add(string key, object value)
        {
            if (_data.Any(x => x.key == key)) throw new ApplicationException("key already in collection");
            _data.Add((key, value));
        }

        public override string ToString()
        {
            return string.Join("", _data.Select(x => $"[{x.key}:{x.value}]"));
        }

        public IEnumerable<(string key, object value)> Values => _data;
    }

    public class CompositeKeyBuilder : ICanAddKey, ICanAddValue, ICanBuildCompositeKey, ICanAddKeyOrBuild
    {
        private readonly IList<(string key, object value)> source;

        private CompositeKeyBuilder()
        {
            source = new List<(string key, object value)>();
        }

        public static ICanAddKey Create()
        {
            return new CompositeKeyBuilder();
        }

        public ICanAddValue AddKey(string key)
        {
            key = Preconditions.NotEmpty(key, nameof(key));
            if (source.Any(x => string.Equals(x.key, key, StringComparison.InvariantCultureIgnoreCase))) throw new ApplicationException($"{nameof(key)} already in {nameof(source)}");
            var tuple = (key: key, value: default(object));
            source.Add(tuple);
            return this as ICanAddValue;
        }

        public ICanAddKeyOrBuild AddValue(object value)
        {
            value = Preconditions.NotNull(value, nameof(value));
            var last = source.Last();
            var tuple = (key: last.key, value: value);
            source.RemoveAt(source.Count() - 1);
            source.Add(tuple);
            return this as ICanAddKeyOrBuild;
        }

        public CompositeKey Build()
        {
            return new CompositeKey(source);
        }

        public interface ICanAddKey
        {
            ICanAddValue AddKey(string key);
        }

        public interface ICanAddKeyOrBuild : ICanAddKey, ICanBuildCompositeKey
        {
        }

        public interface ICanAddValue
        {
            ICanAddKeyOrBuild AddValue(object value);
        }

        public interface ICanBuildCompositeKey
        {
            CompositeKey Build();
        }
    }
}