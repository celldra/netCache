using NetCache.Interfaces;
using Utf8Json;

namespace NetCache
{
    /// <summary>
    ///     The default serializer for netCache implements a UTF-8 JSON serializer with minimal memory allocations
    /// </summary>
    public class DefaultSerializer : ISerializer
    {
        public ReadOnlySpan<char> Serialize<TObj>(TObj obj)
        {
            return JsonSerializer.ToJsonString(obj).AsSpan();
        }

        public TObj Deserialize<TObj>(ReadOnlySpan<char> rawData)
        {
            return JsonSerializer.Deserialize<TObj>(rawData.ToString());
        }
    }
}