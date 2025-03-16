namespace BusinessLayer.Interface
{
    public interface ICacheService
    {
        Task<string> GetCachedData(string key);
        Task SetCachedData(string key, string value);
        Task RemoveCachedData(string key);
    }
}
