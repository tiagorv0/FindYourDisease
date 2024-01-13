namespace FindYourDisease.Users.Application.Caching;
public interface ICache
{
    abstract string SetCacheKey(long id);
}
