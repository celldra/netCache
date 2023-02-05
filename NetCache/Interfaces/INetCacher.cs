namespace NetCache.Interfaces
{
    public interface INetCacher
    {
        /// <summary>
        ///     Gets an object from the cache
        /// </summary>
        /// <param name="key">The object's key</param>
        /// <typeparam name="TObj">The type of object to deserialize to</typeparam>
        /// <returns>The object if found, default otherwise</returns>
        TObj? GetObject<TObj>(string key);

        /// <summary>
        ///     Sets an object in the cache
        /// </summary>
        /// <param name="key">The key to use</param>
        /// <param name="obj">The object value to set</param>
        /// <param name="lifetime">The optional lifetime of the object</param>
        /// <typeparam name="TObj">The type of object used in the value</typeparam>
        void SetObject<TObj>(string key, TObj obj, TimeSpan? lifetime = null);
    }
}