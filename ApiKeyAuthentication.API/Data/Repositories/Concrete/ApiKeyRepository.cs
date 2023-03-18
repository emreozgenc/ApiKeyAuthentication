using ApiKeyAuthentication.API.Data.Entities;
using ApiKeyAuthentication.API.Data.EntityFramework;
using ApiKeyAuthentication.API.Data.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace ApiKeyAuthentication.API.Data.Repositories.Concrete
{
    public class ApiKeyRepository : IApiKeyRepository
    {
        private readonly AppDbContext _context;
        public ApiKeyRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(ApiKey apiKey, ICollection<int> permissionIds)
        {
            apiKey.Permissions = new List<ApiKeyPermission>();

            foreach (var id in permissionIds)
            {
                apiKey.Permissions.Add(new ApiKeyPermission() { PermissionId = id });
            }

            await _context.ApiKeys.AddAsync(apiKey);
            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<string>> GetPermissionsAsync(string apiKey)
        {
            return await _context.ApiKeyPermissions
                .Include(x => x.ApiKey)
                .Include(x => x.Permission)
                .Where(x => x.ApiKey.Value == apiKey)
                .Select(x => x.Permission.Value)
                .ToListAsync();
        }
    }
}
