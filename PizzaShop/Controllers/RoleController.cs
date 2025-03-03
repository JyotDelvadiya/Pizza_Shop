using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using PizzaShop.Business.Interface;
using Microsoft.AspNetCore.Authorization;
using PizzaShop.Data.ViewModel;
using PizzaShop.Data.Data;
using Microsoft.EntityFrameworkCore;
using PizzaShop.Data.Models;

public class RoleController : Controller
{
    private readonly IRoleService _roleService;
    private readonly PizzaShopDbContext _context;

    public RoleController(IRoleService roleService, PizzaShopDbContext context)
    {
        _roleService = roleService;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var roles = await _roleService.GetAllRolesAsync();
        return View(roles);
    }

    public async Task<IActionResult> Permissions(int roleId)
    {
        var role = await _roleService.GetPermissionMatrixForRoleAsync(roleId);
        if (role == null) return NotFound();

        return View(role);
    }

    [Authorize(Roles = "Admin, Account Manager")]
    [HttpPost]
    public async Task<IActionResult> UpdatePermissions([FromBody] PermissionMatrixVM model)
    {
        bool result = await _roleService.UpdatePermissionAsync(model);
        if (result)
        {
            return Json(new { success = true, message = "Permissions updated successfully!"});
        }
        else
        {
            return Json(new { success = false, message = "An error occurred while updating permissions."});
        }
    }
}