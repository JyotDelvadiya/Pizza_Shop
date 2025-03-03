using PizzaShop.Data.Models;

namespace PizzaShop.Business.Interface
{
    public interface IRoleRepository
    {
        Task<Role?> GetByIdAsync(int roleId);
        IAsyncEnumerable<Permission?> GetAll();
        IEnumerable<Rolepermission> GetByRoleId(int roleId);
    }
}