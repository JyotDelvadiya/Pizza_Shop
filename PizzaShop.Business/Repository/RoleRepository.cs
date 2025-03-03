using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Bcpg;
using PizzaShop.Business.Interface;
using PizzaShop.Data.Data;
using PizzaShop.Data.Models;

namespace PizzaShop.Business.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private readonly PizzaShopDbContext _context;
        public RoleRepository(PizzaShopDbContext context)
        {
            _context = context;
        }
        public async Task<Role?> GetByIdAsync(int roleId)
        {
            return await _context.Roles.FirstOrDefaultAsync(r => r.Roleid == roleId);
        }
        public async IAsyncEnumerable<Permission?> GetAll()
        {
            var permissions = await _context.Permissions.ToListAsync();
            foreach (var permission in permissions)
            {
                yield return permission;
            }
        }
        public IEnumerable<Rolepermission> GetByRoleId(int roleId)
        {
            return _context.Rolepermissions.Where(p => p.Roleid == roleId).ToList();
        }
    }
}