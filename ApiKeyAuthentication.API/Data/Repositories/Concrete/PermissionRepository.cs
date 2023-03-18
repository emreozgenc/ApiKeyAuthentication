using ApiKeyAuthentication.API.Data.Entities;
using ApiKeyAuthentication.API.Data.EntityFramework;
using ApiKeyAuthentication.API.Data.Repositories.Abstract;

namespace ApiKeyAuthentication.API.Data.Repositories.Concrete
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly AppDbContext _context;

        public PermissionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Permission permission)
        {
            await _context.AddAsync(permission);
            await _context.SaveChangesAsync();
        }
    }
}
