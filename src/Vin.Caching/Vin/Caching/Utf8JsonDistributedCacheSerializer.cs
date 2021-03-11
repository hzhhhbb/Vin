using System.Text.Json;

namespace Vin.Caching
{
    public class Utf8JsonDistributedCacheSerializer : IDistributedCacheSerializer
    {
        public byte[] Serialize<T>(T obj)
        {
            return JsonSerializer.SerializeToUtf8Bytes(obj);
        }

        public T Deserialize<T>(byte[] bytes)
        {
            return JsonSerializer.Deserialize<T>(bytes);
        }
    }
}