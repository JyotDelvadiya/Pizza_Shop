using Microsoft.EntityFrameworkCore;
using PizzaShop.Business.Interface;
using PizzaShop.Data;
using PizzaShop.Data.Data;
using PizzaShop.Data.Models;
using System.Threading.Tasks;

namespace PizzaShop.Business.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly PizzaShopDbContext _context;

        public AccountRepository(PizzaShopDbContext context)
        {
            _context = context;
        }

        public async Task<Account?> GetAccountByEmailAsync(string email)
        {
            return await _context.Accounts.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> GetUserByAccountIdAsync(int accountId)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Accountid == accountId);
        }

        public async Task<Role?> GetRoleByIdAsync(int roleId)
        {
            return await _context.Roles.FirstOrDefaultAsync(r => r.Roleid == roleId);
        }

        public async Task<bool> UpdateAccountPasswordAsync(Account account)
        {
            _context.Accounts.Update(account);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
