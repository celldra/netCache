using System;
using NetCache.Handlers;
using NetCache.Interfaces;
using NetCache.Models;

namespace NetCache;

/// <summary>
///     The main object for netCache that performs cache set and get operations
/// </summary>
public class NetCacher : INetCacher
{
    /// <summary>
    ///     The options to use for caching objects
    /// </summary>
    private readonly NetCacheOptions _options;

    /// <summary>
    ///     Handler for caching objects with persistence to the disk
    /// </summary>
    private readonly DiskCache _diskCache;

    public NetCacher(NetCacheOptions options)
    {
        _options = options;
        _diskCache = new DiskCache(options); // Init new disk cacher
    }

    /// <summary>
    ///     Gets an object from the cache
    /// </summary>
    /// <param name="key">The object's key</param>
    /// <typeparam name="TObj">The type of object to deserialize to</typeparam>
    /// <returns>The object if found, default otherwise</returns>
    public TObj? GetObject<TObj>(string key)
    {
        var validKey = Utils.Hash(key);
        var val = new CachedObject(); // Set default value

        if (MemoryCache.Dictionary.ContainsKey(validKey)) // Check if it exists in memory cache 
        {
            val = MemoryCache.Dictionary[validKey]; // Set value
        }
        else if (_options.IsPersistant) // Only go to persistant store if it's enabled
        {
            val = _diskCache.Get(validKey); // Get from disk cache

            if (val is not null) // Add it to mem cache if not null
                MemoryCache.Dictionary[validKey] = val;
        }

        // Check validity
        return val?.IsValid() == false
            ? default
            :
            // Pass it to the serializer and return
            _options.Serializer.Deserialize<TObj>(val?.Value);
    }

    /// <summary>
    ///     Sets an object in the cache
    /// </summary>
    /// <param name="key">The key to use</param>
    /// <param name="obj">The object value to set</param>
    /// <param name="lifetime">The optional lifetime of the object</param>
    /// <typeparam name="TObj">The type of object used in the value</typeparam>
    public void SetObject<TObj>(string key, TObj obj, TimeSpan? lifetime = null)
    {
        lifetime ??= _options.Lifetime; // Set default lifetime if null
        var validKey = Utils.Hash(key); // Hash key

        var o = new CachedObject // Create cached object
        {
            Value = _options.Serializer.Serialize(obj).ToString(),
            ExpireTime = new DateTimeOffset(DateTime.UtcNow + lifetime.Value).ToUnixTimeMilliseconds()
        };

        // Store in mem cache
        MemoryCache.Dictionary[validKey] = o;

        // Store in disk cache if enabled
        if (_options.IsPersistant) _diskCache.Set(validKey, o);
    }
}