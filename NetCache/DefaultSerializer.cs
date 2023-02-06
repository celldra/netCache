using System.Text.Json;
using NetCache.Interfaces;

namespace NetCache
{
    /// <summary>
    ///     The default serializer for netCache implements System.Text.Json
    /// </summary>
    public class DefaultSerializer : ISerializer
    {
        public ReadOnlySpan<char> Serialize<TObj>(TObj obj)
        {
            var json = JsonSerializer.Serialize(obj).AsSpan();
            return json;
        }

        public TObj? Deserialize<TObj>(ReadOnlySpan<char> rawData)
        {
            return rawData.IsEmpty ? default : // Disallow empty input
                JsonSerializer.Deserialize<TObj>(rawData);
        }
    }
}