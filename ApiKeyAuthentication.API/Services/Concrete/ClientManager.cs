using ApiKeyAuthentication.API.Cache.Abstract;
using ApiKeyAuthentication.API.Data.Repositories.Abstract;
using ApiKeyAuthentication.API.Services.Abstract;

namespace ApiKeyAuthentication.API.Services.Concrete
{
    public class ClientManager : IClientService
    {
        private readonly IApiKeyRepository _apiKeyRepository;
        private readonly ICacheService _cacheService;

        public ClientManager(IApiKeyRepository apiKeyRepository, ICacheService cacheService)
        {
            _apiKeyRepository = apiKeyRepository;
            _cacheService = cacheService;
        }

        public Task<string> GenerateApiKeyAsync(ICollection<int> permissionIds)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<string>> GetPermissionAsync(string apiKey)
        {
            var cachedItem = _cacheService.Get<ICollection<string>>(apiKey);
           
            if(cachedItem is null)
            {
                cachedItem = await _apiKeyRepository.GetPermissionsAsync(apiKey);
                _cacheService.AddCache(apiKey, cachedItem);
            }

            return cachedItem;
        }
    }
}
