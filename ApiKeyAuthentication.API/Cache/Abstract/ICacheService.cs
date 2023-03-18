namespace ApiKeyAuthentication.API.Cache.Abstract
{
    public interface ICacheService
    {
        void AddCache(object key, object obj);
        TObject Get<TObject>(object key);
        void RemoveCache(object key);
    }
}
