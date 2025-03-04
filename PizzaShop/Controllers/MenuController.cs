using System.ComponentModel.Design;
using Microsoft.AspNetCore.Mvc;
using PizzaShop.Data.ViewModel;

public class MenuController : Controller
{
    private readonly IMenuService _menuService;
    public MenuController(IMenuService menuService)
    {
        _menuService = menuService;
    } 

    public async Task<IActionResult> Index()
    {
        var menu = await _menuService.GetAllCategoriesAsync();
        return View(menu);
    }

    [HttpPost]
    public async Task<IActionResult> AddCategory(MenuCategoryVM menuCategoryVM)
    {
        var result = await _menuService.AddCategoryAsync(menuCategoryVM);
        return RedirectToAction("Index");
    }
}