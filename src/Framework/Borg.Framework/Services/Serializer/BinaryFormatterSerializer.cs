using Borg.Infrastructure.Core.DI;
using Borg.Infrastructure.Core.Services.Serializer;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace Borg.Framework.Services.Serializer
{
    [PlugableService(ImplementationOf = typeof(ISerializer), Lifetime = Lifetime.Singleton, OneOfMany =true, Order =99)]
    public class BinaryFormatterSerializer : ISerializer
    {


        public BinaryFormatterSerializer()
        {

        }

        public Task<object> DeserializeAsync(byte[] value, Type objectType)
        {
            var formater = new BinaryFormatter();
            Stream stream = new MemoryStream(value);
            var result = formater.Deserialize(stream);
            return Task.FromResult(result);
        }

        public Task<byte[]> SerializeAsync(object value)
        {
            byte[] result;
            using (var stream = new MemoryStream())
            {
                var formater = new BinaryFormatter();
                formater.Serialize(stream, value);
                result = stream.ToArray();
            }
            return Task.FromResult(result);
        }
    }
}