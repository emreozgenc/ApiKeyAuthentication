using ApiKeyAuthentication.API.Data.Entities;

namespace ApiKeyAuthentication.API.Data.Repositories.Abstract
{
    public interface IPermissionRepository
    {
        Task AddAsync(Permission permission);
    }
}
