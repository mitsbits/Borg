using Borg.Infrastructure.Core.DDD.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace Borg.Framework.Azure.Storage.Tables
{
    public class AzureTableCompositeKey : CompositeKey<string>
    {
        public AzureTableCompositeKey():base()
        {
            var a = new List<(string partition, string row)>(new[] { (partition: "xcx", row: "xzv") });
        }
        public AzureTableCompositeKey(string partition, string row) : base(
            new List<(string key, string value)>(new[] { (partition: partition, row: row) }))
        {

        }

        public string Partition => _data.FirstOrDefault(x => x.key == nameof(Partition)).value;
        public string Row => _data.FirstOrDefault(x => x.key == nameof(Row)).value;
    }
}
