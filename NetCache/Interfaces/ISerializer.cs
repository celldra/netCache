using System;

namespace NetCache.Interfaces
{
    /// <summary>
    /// Interface that can be used to implement a serializer for object types
    /// </summary>
    public interface ISerializer
    {
        /// <summary>
        /// Serializes the object to a set of bytes
        /// </summary>
        /// <param name="obj">The object to serialize</param>
        /// <typeparam name="TObj">The type of object that will be serialized</typeparam>
        /// <returns>Set of serialized bytes</returns>
        ReadOnlySpan<char> Serialize<TObj>(TObj obj);
        
        /// <summary>
        /// Deserializes the object from a set of bytes to a .NET object
        /// </summary>
        /// <param name="rawData">The raw data to deserialize</param>
        /// <typeparam name="TObj">The type of object to deserialize to</typeparam>
        /// <returns>The .NET object</returns>
        TObj? Deserialize<TObj>(ReadOnlySpan<char> rawData);
    }
}