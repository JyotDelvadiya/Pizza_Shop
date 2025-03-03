using PizzaShop.Data.Models;
using System.Threading.Tasks;

namespace PizzaShop.Business.Interface
{
    public interface IAccountRepository
    {
        Task<Account?> GetAccountByEmailAsync(string email);
        Task<User?> GetUserByAccountIdAsync(int accountId);
        Task<Role?> GetRoleByIdAsync(int roleId);
        Task<bool> UpdateAccountPasswordAsync(Account account);
        Task SaveChangesAsync();
    }
}