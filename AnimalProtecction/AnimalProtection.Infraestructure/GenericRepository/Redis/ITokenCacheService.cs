namespace AnimalProtecction.GenericRepository.Redis;

public interface ITokenCacheRepository
{
    Task<string> GetTokenAsync(string userId);
    Task SetTokenAsync(string userId, string token, TimeSpan expiration);
    Task RemoveTokenAsync(string userId);
}