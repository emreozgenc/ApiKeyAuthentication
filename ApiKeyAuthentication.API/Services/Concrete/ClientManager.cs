using ApiKeyAuthentication.API.Data.Repositories.Abstract;
using ApiKeyAuthentication.API.Services.Abstract;

namespace ApiKeyAuthentication.API.Services.Concrete
{
    public class ClientManager : IClientService
    {
        private readonly IApiKeyRepository _apiKeyRepository;

        public ClientManager(IApiKeyRepository apiKeyRepository)
        {
            _apiKeyRepository = apiKeyRepository;
        }

        public Task<string> GenerateApiKeyAsync(ICollection<int> permissionIds)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<string>> GetPermissionAsync(string apiKey)
        {
            return await _apiKeyRepository.GetPermissionsAsync(apiKey);
        }
    }
}
