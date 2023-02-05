using System.Runtime.Serialization;

namespace NetCache.Models;

internal class CachedObject
{
    /// <summary>
    ///     Value of the object
    /// </summary>
    [DataMember(Name = "v")]
    public string Value { get; set; } = string.Empty;

    /// <summary>
    ///     The pre-calculated date when the entry will expire
    /// </summary>
    [DataMember(Name = "e")]
    public long ExpireTime { get; set; }

    /// <summary>
    ///     Determines whether a cached object is valid or not based on the timestamp
    /// </summary>
    /// <returns>True if the object is valid, false otherwise</returns>
    public bool IsValid()
    {
        // Unix times are faster, I hope (will benchmark later)
        var unixTime = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds();
        return unixTime <= ExpireTime; // Return whether the expire time is greater than the current Unix timestamp
    }
}