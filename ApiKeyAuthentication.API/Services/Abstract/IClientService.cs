namespace ApiKeyAuthentication.API.Services.Abstract
{
    public interface IClientService
    {
        Task<string> GenerateApiKeyAsync(ICollection<int> permissionIds);
        Task<ICollection<string>> GetPermissionAsync(string apiKey);
    }

}
