using System;
using System.Threading.Tasks;

namespace Borg.Infrastructure.Core.Services.Serializer
{
    public interface ISerializer
    {
        Task<object> DeserializeAsync(byte[] data, Type objectType);

        Task<byte[]> SerializeAsync(object value);
    }
}