using System.Text;
using System.Text.Json;
using IronSnappy;
using NetCache.Interfaces;

namespace NetCache
{
    /// <summary>
    ///     The default serializer for netCache implements Snappy compressed System.Text.Json
    /// </summary>
    public class DefaultSerializer : ISerializer
    {
        public ReadOnlySpan<char> Serialize<TObj>(TObj obj)
        {
            var json = JsonSerializer.SerializeToUtf8Bytes(obj).AsSpan();
            var compressed = Snappy.Encode(json).AsSpan();
            return Encoding.UTF8.GetString(compressed).AsSpan();
        }

        public TObj? Deserialize<TObj>(ReadOnlySpan<char> rawData)
        {
            if (rawData.IsEmpty) return default; // Disallow empty input
            
            var uncompressed = Snappy.Decode(Encoding.UTF8.GetBytes(rawData.ToString()).AsSpan());
            return JsonSerializer.Deserialize<TObj>(uncompressed);
        }
    }
}