using System;
using System.IO;
using NetCache.Interfaces;

namespace NetCache.Models
{
    /// <summary>
    ///     Represents a set of configurable options that the cacher will use
    /// </summary>
    public class NetCacheOptions
    {
        /// <summary>
        ///     The prefix to append to every object, defaults to an empty string
        /// </summary>
        public string Prefix { get; set; } = string.Empty;

        /// <summary>
        ///     The default lifetime of every object when one is not set, defaults to 1 hour
        /// </summary>
        public TimeSpan Lifetime { get; set; } = TimeSpan.FromHours(1);

        /// <summary>
        ///     Whether the object is persistant to the disk or not, defaults to true
        /// </summary>
        public bool IsPersistant { get; set; } = true;

        /// <summary>
        /// The location to use when storing persistant cache files, defaults to a folder in the current directory called 'cache'
        /// </summary>
        public string PersistantCacheLocation { get; set; } = Path.Combine(Environment.CurrentDirectory, "cache");

        /// <summary>
        ///     The data serializer to use when getting and setting data, defaults to <see cref="DefaultSerializer"/>
        /// </summary>
        public ISerializer Serializer { get; set; } = new DefaultSerializer();

        // /// <summary>
        // ///     Whether to enable the built-in garbage collector which will periodically check over every record and remove expired
        // ///     records from the cache - this may significantly slow down the cache system, defaults to false
        // /// </summary>
        // public bool IsGarbageCollectorEnabled { get; set; } = false; TODO: Impl
    }
}