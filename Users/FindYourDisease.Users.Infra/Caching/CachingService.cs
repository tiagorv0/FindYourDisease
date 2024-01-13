using FindYourDisease.Users.Domain.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace FindYourDisease.Users.Infra.Caching;
public class CachingService : ICachingService
{
    private readonly IDistributedCache _cache;
    private readonly DistributedCacheEntryOptions _options;

    public CachingService(IDistributedCache cache)
    {
        _cache = cache;
        _options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5),
            SlidingExpiration = TimeSpan.FromMinutes(5),
        };
    }

    public async Task<string?> GetAsync(string key)
    {
        return await _cache.GetStringAsync(key);
    }

    public async Task SetAsync<T>(string key, T entity)
    {
        var value = JsonSerializer.Serialize(entity);
        await _cache.SetStringAsync(key, value, _options);
    }

    public async Task RemoveAsync(string key)
    {
        var cache = await GetAsync(key);

        if (cache is not null)
            await _cache.RemoveAsync(key);
    }
}
