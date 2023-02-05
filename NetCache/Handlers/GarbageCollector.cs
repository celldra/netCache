using System.Runtime.InteropServices;
using NetCache.Models;

namespace NetCache.Handlers;

/// <summary>
/// The garbage collector for usage with netCache
/// </summary>
internal class GarbageCollector
{
    private readonly NetCacheOptions _options;
    private readonly DiskCache _diskCache;

    public GarbageCollector(NetCacheOptions options)
    {
        _options = options;
        _diskCache = new DiskCache(options);
    }

    /// <summary>
    /// Runs the garbage collection routine for disk caching
    /// </summary>
    public void RunDisk()
    {
        var files = Directory.GetFiles(_options.PersistantCacheLocation); // Get all files in the persistant cache location
        foreach (var file in files) // Enumerate over every cache file
        {
            var fileName = Path.GetFileNameWithoutExtension(file); // Get file name
            var key = ulong.Parse(fileName); // Parse file name to a valid key

            var o = _diskCache.Get(key); // Get from the disk cache
            if (o?.IsValid() == false) // Check if the entry is valid
            {
                // Remove it since it is not valid
                File.Delete(file);
            }
        }
    }
}