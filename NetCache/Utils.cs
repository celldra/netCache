using System.Text;
using Extensions.Data;

namespace NetCache;

internal static class Utils
{
    /// <summary>
    /// Hashes a key to the correct value
    /// </summary>
    /// <param name="key">The key to hash</param>
    /// <returns>The hashed value</returns>
    internal static ulong Hash(string key)
    {
        var input = Encoding.UTF8.GetBytes(key); 
        return XXHash.XXH64(input);
    }
}