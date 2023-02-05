using Microsoft.Extensions.DependencyInjection;
using NetCache.Interfaces;
using NetCache.Models;

namespace NetCache.DependencyInjection;

public static class ServiceExtensions
{
    /// <summary>
    ///     Adds netCache to the service collection
    /// </summary>
    /// <param name="collection">The service collection to modify</param>
    /// <param name="options">The options to use when configuring netCache</param>
    /// <returns>The modified service collection</returns>
    public static IServiceCollection AddNetCache(this IServiceCollection collection, Action<NetCacheOptions> options)
    {
        // Set options
        var opts = new NetCacheOptions();
        options(opts);

        // Inject
        return collection.AddSingleton<INetCacher>(new NetCacher(opts));
    }
}