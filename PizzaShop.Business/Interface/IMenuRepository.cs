using PizzaShop.Data.Models;
using PizzaShop.Data.ViewModel;

public interface IMenuRepository
{
   IAsyncEnumerable<Category> GetAllCategoriesAsync();
   Task<bool> AddCategoryAsync(Category category);
}