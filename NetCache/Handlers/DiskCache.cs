using System.Text;
using Extensions.Data;
using NetCache.Models;

namespace NetCache.Handlers;

/// <summary>
///     Persistant disk cache handler, does not care about lifetime verification
/// </summary>
internal class DiskCache
{
    private readonly NetCacheOptions _options;

    public DiskCache(NetCacheOptions options)
    {
        _options = options;

        // Ensure that the specified location exists
        if (!Directory.Exists(_options.PersistantCacheLocation))
            Directory.CreateDirectory(_options.PersistantCacheLocation);
    }

    public CachedObject? Get(ulong key)
    {
        var location = Path.Combine(_options.PersistantCacheLocation, key.ToString()); // Make path
        CachedObject? obj;

        // ReSharper disable once ConvertToUsingDeclaration
        using (var stream = File.OpenRead(location))
        {
            var bytes = new Span<byte>(); // New raw file byte span
            _ = stream.Read(bytes); // Read to a span

            var chars = Encoding.UTF8.GetChars(bytes.ToArray()).AsSpan(); // Get as chars
            obj = _options.Serializer.Deserialize<CachedObject>(chars); // Deserialize
        }
        
        return obj; // Return obj
    }

    public void Set(ulong key, CachedObject cachedObject)
    {
        var location = Path.Combine(_options.PersistantCacheLocation, key.ToString()); // Make path

        var serialized = _options.Serializer.Serialize(cachedObject); // Serialize object
        File.WriteAllText(location, serialized.ToString()); // Write serialized
    }
}