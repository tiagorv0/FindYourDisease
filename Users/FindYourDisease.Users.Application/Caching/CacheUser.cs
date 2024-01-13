namespace FindYourDisease.Users.Application.Caching;
public abstract class CacheUser : ICache
{
    public string SetCacheKey(long id) => "User_" + id.ToString();
}
