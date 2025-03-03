// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using PizzaShop.Business.Interface;
// using PizzaShop.Data.Models;

// public class PermissionService : IPermissionService
// {
//     private readonly IPermissionService _permissionService;
//     private readonly IRolePermissionService _rolePermissionService;

//     public PermissionService(IPermissionService permissionService, IRolePermissionService rolePermissionService)
//     {
//         _permissionService = permissionService;
//         _rolePermissionService = rolePermissionService;
//     }

//     public async Task<IEnumerable<Permission>> GetAllPermissionsAsync()
//     {
//         return await _permissionService.GetAllPermissionsAsync();
//     }

//     public async Task<IEnumerable<Rolepermission>> GetPermissionsByRoleIdAsync(int roleId)
//     {
//         return await _rolePermissionService.GetPermissionsByRoleIdAsync(roleId);
//     }

//     public async Task AssignPermissionsToRoleAsync(int roleId, List<int> permissionIds)
//     {
//         await _rolePermissionService.AssignPermissionsToRoleAsync(roleId, permissionIds);
//     }
// }
