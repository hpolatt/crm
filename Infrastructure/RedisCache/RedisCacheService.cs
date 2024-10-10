using System;
using Core.Application.Interfaces.RedisCache;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Infrastructure.RedisCache;

public class RedisCacheService : IRedisCacheService
{
    private readonly RedisCacheSettings settings;

    private readonly ConnectionMultiplexer redisConnetion;

    private readonly IDatabase database;

    public RedisCacheService(IOptions<RedisCacheSettings> options)
    {
        this.settings = options.Value;
        var opt = ConfigurationOptions.Parse(settings.ConnectionString);
        redisConnetion = ConnectionMultiplexer.Connect(opt);
        database = redisConnetion.GetDatabase();
    }

    public async Task<T> GetAsync<T>(string key)
    {
        var value = await database.StringGetAsync(key);
        if (value.HasValue) {
            return JsonConvert.DeserializeObject<T>(value);
        }
        return default;
    }

    public Task SetAsync<T>(string key, T value, DateTime? expirationTime = null)
    {
        var json = JsonConvert.SerializeObject(value);

        TimeSpan? expTime = expirationTime.HasValue ? expirationTime.Value - DateTime.Now :  null;
        return database.StringSetAsync(key, json, expTime);
    }
}
