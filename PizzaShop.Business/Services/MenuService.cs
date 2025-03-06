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

    public async Task<MenuCategoryVM> GetAllCategoriesAsync(int categoryId)
    {
        var allCategories = _menuRepository.GetAllCategoriesAsync();
        var allMenuItems = _menuRepository.GetMenuItemsAsync(categoryId);

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
        await foreach (var menuItem in allMenuItems)
        {
            var MenuItem = new Menuitem
            {
                Menuitemid = menuItem.Menuitemid,
                Itemname = menuItem.Itemname,
                Categoryid = menuItem.Categoryid,
                Itemtype = menuItem.Itemtype,
                Rate = menuItem.Rate,
                Quantity = menuItem.Quantity,
                Status = menuItem.Status,
                Description = menuItem.Description,
                Itemimage = menuItem.Itemimage,
                Taxpercentage = menuItem.Taxpercentage,
                Shortcode = menuItem.Shortcode,
                Defaulttax = menuItem.Defaulttax,
                Unitid = menuItem.Unitid
            };

            menuCategory.Menuitems.Add(MenuItem);
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

        var existingCategory = await _menuRepository.CheckCategoryExistanceAsync(category);
        if (existingCategory == null)
        {
            await _menuRepository.AddCategoryAsync(category);
            return true;
        }
        else
        {
            return false;
        }
    }

    public async Task<bool> EditCategoryAsync(MenuCategoryVM menuCategoryVM)
    {
        var category = new Category
        {
            Categoryid = menuCategoryVM.categoryId,
            Categoryname = menuCategoryVM.Categoryname,
            Categorydescription = menuCategoryVM.Categorydescription
        };
        var existingCategory = await _menuRepository.CheckCategoryExistanceExceptGivenAsync(category);
        if (existingCategory == null)
        {
            await _menuRepository.EditCategoryAsync(category);
            return true;
        }
        else
        {
            return false;
        }
    }

    public async Task<bool> DeleteCategoryAsync(int categoryId)
    {
        var result = await _menuRepository.DeleteCategoryAsync(categoryId);
        return result ? true : false;
    }
}