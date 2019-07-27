using System;
using System.Threading.Tasks;

namespace Borg.Infrastructure.Core.Services.Serializer
{
    public interface ISerializer
    {
        Task<object> Deserialize(byte[] data, Type objectType);

        Task<byte[]> Serialize(object value);
    }
}