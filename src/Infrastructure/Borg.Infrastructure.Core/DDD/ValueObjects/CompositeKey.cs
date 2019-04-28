using System;
using System.Collections.Generic;
using System.Linq;

namespace Borg.Infrastructure.Core.DDD.ValueObjects
{
    [Serializable]
    public class CompositeKey : CompositeKey<object>
    {
    }

    public class CompositeKey<TKey> : ValueObject<CompositeKey<TKey>>
    {
        protected readonly List<(string key, TKey value)> _data;

        public CompositeKey(IEnumerable<(string key, TKey value)> source)
        {
            _data = new List<(string key, TKey value)>();
            foreach (var incoming in source)
            {
                Add(incoming.key, incoming.value);
            }
        }

        public CompositeKey()
        {
            _data = new List<(string key, TKey value)>();
        }

        public virtual void Add(string key, TKey value)
        {
            if (_data.Any(x => x.key == key)) throw new System.Exception("key already in collection");
            _data.Add((key, value));
        }

        public override string ToString()
        {
            return string.Join("", _data.Select(x => $"[{x.key}:{x.value}]"));
        }

        public IEnumerable<(string key, TKey value)> Values => _data;
    }
}