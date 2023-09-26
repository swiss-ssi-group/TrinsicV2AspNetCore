using System.Text.Json.Serialization;
using Microsoft.Extensions.Caching.Distributed;

namespace TrinsicV2WebWallet;

public class CacheData
{
    [JsonPropertyName("authenticateChallenge")]
    public string AuthenticateChallenge { get; set; } = string.Empty;

    public static void AddToCache(string key, IDistributedCache cache, CacheData cacheData)
    {
        var cacheExpirationInHours = 1;
        var options = new DistributedCacheEntryOptions()
            .SetSlidingExpiration(TimeSpan.FromHours(cacheExpirationInHours));
        cache.SetString(key, System.Text.Json.JsonSerializer.Serialize(cacheData), options);
    }

    public static CacheData? GetFromCache(string key, IDistributedCache cache)
    {
        var item = cache.GetString(key);
        if (item != null)
        {
            return System.Text.Json.JsonSerializer.Deserialize<CacheData>(item);
        }

        return null;
    }

    public static void RemoveFromCache(string key, IDistributedCache cache)
    {
        var item = cache.GetString(key);
        if (item != null)
        {
            cache.Remove(key);
        }
    }
}
