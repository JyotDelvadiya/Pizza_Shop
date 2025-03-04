using System.Reflection.Metadata.Ecma335;
using PizzaShop.Data.Models;
using PizzaShop.Data.ViewModel;

public class MenuService : IMenuService
{
    private readonly IMenuRepository _menuRepository;
    public MenuService(IMenuRepository menuRepository)
    {
        _menuRepository = menuRepository;
    }

    public async Task<MenuCategoryVM> GetAllCategoriesAsync()
    {
        var allCategories = _menuRepository.GetAllCategoriesAsync();

        var menuCategory = new MenuCategoryVM();

        await foreach (var category in allCategories)
        {
            var CategoryItem = new Category
            {
                Categoryid = category.Categoryid,
                Categoryname = category.Categoryname,
                Categorydescription = category.Categorydescription
            };

            menuCategory.Categories.Add(CategoryItem);
        }

        return menuCategory;
    } 
    
    public async Task<bool> AddCategoryAsync(MenuCategoryVM menuCategoryVM)
    {
        var category = new Category
        {
            Categoryname = menuCategoryVM.Categoryname,
            Categorydescription = menuCategoryVM.Categorydescription
        };

        var result = await _menuRepository.AddCategoryAsync(category);
        return result;
    }
}