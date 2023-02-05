using System.Collections.Concurrent;
using NetCache.Models;

namespace NetCache.Handlers;

internal static class MemoryCache
{
    public static readonly ConcurrentDictionary<ulong, CachedObject> Dictionary = new();
}