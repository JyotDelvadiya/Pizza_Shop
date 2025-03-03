// using Microsoft.AspNetCore.Mvc;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using PizzaShop.Business.Interface;
// using PizzaShop.Data.ViewModel;

// public class PermissionController : Controller
// {
//     private readonly IPermissionService _permissionService;
//     private readonly IRoleService _roleService;

//     public PermissionController(IPermissionService permissionService, IRoleService roleService)
//     {
//         _permissionService = permissionService;
//         _roleService = roleService;
//     }

//     public async Task<IActionResult> Index(int roleId)
//     {
//         var role = await _roleService.GetRoleByIdAsync(roleId);
//         if (role == null) return NotFound();

//         var permissions = await _permissionService.GetAllPermissionsAsync();
//         var assignedPermissions = await _permissionService.GetPermissionsByRoleIdAsync(roleId);

//         var viewModel = new RolePermissionViewModel
//         {
//             RoleId = roleId,
//             RoleName = role.Name,
//             Permissions = permissions,
//             AssignedPermissionIds = assignedPermissions.Select(rp => rp.PermissionId).ToList()
//         };

//         return View(viewModel);
//     }

//     [HttpPost]
//     public async Task<IActionResult> AssignPermissions(int roleId, List<int> selectedPermissions)
//     {
//         await _permissionService.AssignPermissionsToRoleAsync(roleId, selectedPermissions);
//         return RedirectToAction("Index", new { roleId });
//     }
// }
