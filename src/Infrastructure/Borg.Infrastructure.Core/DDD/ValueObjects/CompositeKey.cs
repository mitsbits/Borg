using System.Collections.Generic;
using System.Linq;

namespace Borg.Infrastructure.Core.DDD.ValueObjects
{
    public class CompositeKey : ValueObject<CompositeKey>
    {
        private readonly List<(string key, object value)> _data;

        public CompositeKey()
        {
            _data = new List<(string key, object value)>();
        }

  

        public void Add(string key, object value)
        {
            _data.Add((key, value));
        }

        private IEnumerable<(string key, object value)> Keys => _data;

        public override string ToString()
        {
            return string.Join("", _data.Select(x => $"[{x.key}:{x.value}]"));
        }
    }
}