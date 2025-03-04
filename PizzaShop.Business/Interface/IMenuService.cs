
using PizzaShop.Data.ViewModel;

public interface IMenuService
{
    Task<MenuCategoryVM> GetAllCategoriesAsync();
    Task<bool> AddCategoryAsync(MenuCategoryVM menuCategoryVM);
}