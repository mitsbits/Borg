using Borg.Infrastructure.Core.DDD.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Borg.Framework.Azure.Storage.Tables
{
    public class AzureTableCompositeKey : CompositeKey<string>
    {
        public AzureTableCompositeKey() : base()
        {
        }

        public AzureTableCompositeKey(string partition, string row) : base(
            new List<(string key, string value)>(new[] { (partition: partition, row: row) }))
        {
        }

        public override void Add(string key, string value)
        {
            if (key != nameof(Partition) && key != nameof(Row)) throw new InvalidOperationException(nameof(key));
            base.Add(key, value);
        }

        public string Partition => _data.FirstOrDefault(x => x.key == nameof(Partition)).value;
        public string Row => _data.FirstOrDefault(x => x.key == nameof(Row)).value;

        public static AzureTableCompositeKey Create(string partition, string row)
        {
            return new AzureTableCompositeKey(partition, row);
        }
    }
}