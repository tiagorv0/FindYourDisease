namespace FindYourDisease.Users.Application.Caching;
public abstract class CachePatient : ICache
{
    public string SetCacheKey(long id) => "Patient_" + id.ToString();
}
