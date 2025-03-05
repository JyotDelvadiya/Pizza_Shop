using PizzaShop.Data.Models;
using PizzaShop.Data.ViewModel;

public interface IMenuRepository
{
   IAsyncEnumerable<Category> GetAllCategoriesAsync();
   Task<Category?> CheckCategoryExistanceAsync(Category category);
   Task AddCategoryAsync(Category category);
   Task EditCategoryAsync(Category category);
   Task<Category?> CheckCategoryExistanceExceptGivenAsync(Category category);
   Task<bool> DeleteCategoryAsync(int categoryId);
   IAsyncEnumerable<Menuitem> GetMenuItemsAsync(int categoryId);
}