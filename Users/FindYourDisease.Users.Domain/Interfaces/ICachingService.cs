namespace FindYourDisease.Users.Domain.Interfaces;
public interface ICachingService
{
    Task<string?> GetAsync(string key);
    Task SetAsync<T>(string key, T entity);
    Task RemoveAsync(string key);
}
