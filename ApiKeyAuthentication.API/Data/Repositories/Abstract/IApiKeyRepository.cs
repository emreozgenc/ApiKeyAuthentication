using ApiKeyAuthentication.API.Data.Entities;

namespace ApiKeyAuthentication.API.Data.Repositories.Abstract
{
    public interface IApiKeyRepository
    {
        Task AddAsync(ApiKey apiKey, ICollection<int> permissionIds);
        Task<ICollection<string>> GetPermissionsAsync(string apiKey);
    }
}
