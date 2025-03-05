
using PizzaShop.Data.ViewModel;

public interface IMenuService
{
    Task<MenuCategoryVM> GetAllCategoriesAsync(int categoryId);
    Task<bool> AddCategoryAsync(MenuCategoryVM menuCategoryVM);
    Task<bool> EditCategoryAsync(MenuCategoryVM menuCategoryVM);
    Task<bool> DeleteCategoryAsync(int categoryId);
}