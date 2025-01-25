using StackExchange.Redis;

namespace AnimalProtecction.GenericRepository.Redis;

public class TokenCacheRepository: ITokenCacheRepository
{
    private readonly IDatabase _redisDb;

    public TokenCacheRepository(IConnectionMultiplexer redis)
    {
        _redisDb = redis.GetDatabase();
    }

    public async Task<string> GetTokenAsync(string userId)
    {
        return await _redisDb.StringGetAsync($"token:{userId}");
    }

    public async Task SetTokenAsync(string userId, string token, TimeSpan expiration)
    {
        await _redisDb.StringSetAsync($"token:{userId}", token, expiration);
    }

    public async Task RemoveTokenAsync(string userId)
    {
        await _redisDb.KeyDeleteAsync($"token:{userId}");
    }
}