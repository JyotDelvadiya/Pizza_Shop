// using Microsoft.AspNetCore.Mvc;
// using System.Threading.Tasks;
// using PizzaShop.Business.Interface;

// public class RoleController : Controller
// {
//     private readonly IRoleService _roleService;

//     public RoleController(IRoleService roleService)
//     {
//         _roleService = roleService;
//     }

//     public async Task<IActionResult> Index()
//     {
//         var roles = await _roleService.GetAllRolesAsync();
//         return View(roles);
//     }

//     public async Task<IActionResult> ManagePermissions(int roleId)
//     {
//         var role = await _roleService.GetRoleByIdAsync(roleId);
//         if (role == null) return NotFound();

//         return RedirectToAction("Index", "Permission", new { roleId });
//     }
// }