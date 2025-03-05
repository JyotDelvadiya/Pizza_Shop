using System.ComponentModel.Design;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PizzaShop.Data.ViewModel;

public class MenuController : Controller
{
    private readonly IMenuService _menuService;
    public MenuController(IMenuService menuService)
    {
        _menuService = menuService;
    }

    public async Task<IActionResult> Index(int categoryId = 1)
    {
        var menu = await _menuService.GetAllCategoriesAsync(categoryId);
        return View(menu);
    }

    [HttpPost]
    public async Task<IActionResult> AddCategory(MenuCategoryVM menuCategoryVM)
    {
        if(!ModelState.IsValid){
            return Json(new { success = false, message = "Please enter Valid Category." });
        }
        var result = await _menuService.AddCategoryAsync(menuCategoryVM);
        if (result)
        {
            return Json(new { success = true, message = "Category added Successfully!" });
        }
        else
        {
            return Json(new { success = false, message = "Category Already Exists." });
        }
    }

    [HttpPost]
    public async Task<IActionResult> EditCategory(MenuCategoryVM menuCategoryVM)
    {
        if(!ModelState.IsValid){
            return Json(new { success = false, message = "Please enter Valid Category." });
        }
        var result = await _menuService.EditCategoryAsync(menuCategoryVM);
        if (result)
        {
            return Json(new { success = true, message = "Category edited Successfully!" });
        }
        else
        {
            return Json(new { success = false, message = "Category Already Exists." });
        }
    }

    [HttpPost]
    public async Task<IActionResult> DeleteCategory(int categoryId)
    {
        var result = await _menuService.DeleteCategoryAsync(categoryId);
        if (result)
        {
            return Json(new { success = true, message = "Category Deleted Successfully!" });
        }
        else
        {
            return Json(new { success = false, message = "Error Occured While Deleting Category." });
        }
    }
}