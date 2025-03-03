using System.Collections.Generic;
using System.Threading.Tasks;
using PizzaShop.Data.Models;
using PizzaShop.Data.ViewModel;

public interface IRoleService
{
    Task<IEnumerable<Role>> GetAllRolesAsync();
    Task<Role> GetRoleByIdAsync(int roleId);
    Task<PermissionMatrixVM> GetPermissionMatrixForRoleAsync(int roleId);
    Task<bool> UpdatePermissionAsync(PermissionMatrixVM permissionMatrixVM);
    // Task<IEnumerable<RolePermissionVM>> GetPermissionByIdAsync(int roleId);
}