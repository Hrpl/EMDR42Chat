
using EMDR42Chat.Infrastructure.Services.Interfaces;
using StackExchange.Redis;

namespace EMDR42Chat.Infrastructure.Services.Implementations;

public class RedisService : IRedisService
{
    private readonly IDatabase _db;

    public RedisService(IDatabase db)
    {
        _db = db;
    }
    public async Task DeleteAsync(string key)
    {
        await _db.StringGetDeleteAsync(key);
    }

    public async Task<string> GetValueAsync(string key)
    {
        return await _db.StringGetAsync(key);
    }

    public async Task SetValueAsync(string key, string value)
    {
        await _db.StringSetAsync(key, value);
    }
}
