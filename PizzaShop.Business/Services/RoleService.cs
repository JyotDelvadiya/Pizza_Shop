// using System.Collections.Generic;
// using System.Threading.Tasks;
// using PizzaShop.Business.Interface;
// using PizzaShop.Data.Models;

// public class RoleService : IRoleService
// {
//     private readonly IRoleService _roleService;

//     public RoleService(IRoleService roleService)
//     {
//         _roleService = roleService;
//     }

//     public async Task<IEnumerable<Role>> GetAllRolesAsync()
//     {
//         return await _roleService.GetAllRolesAsync();
//     }

//     public async Task<Role> GetRoleByIdAsync(int roleId)
//     {
//         return await _roleService.GetRoleByIdAsync(roleId);
//     }
// }
