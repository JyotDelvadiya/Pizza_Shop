using Microsoft.EntityFrameworkCore;
using PizzaShop.Data.Data;
using PizzaShop.Data.Models;
using PizzaShop.Data.ViewModel;

namespace PizzaShop.Business.Interface
{
    public class MenuRepository : IMenuRepository
    {
        private readonly PizzaShopDbContext _context;
        public MenuRepository(PizzaShopDbContext context)
        {
            _context = context;
        }

        public async IAsyncEnumerable<Category> GetAllCategoriesAsync()
        {
            var categories = await _context.Categories.ToListAsync();
            foreach (var Category in categories)
            {
                yield return Category;
            }
        } 
        public async Task<bool> AddCategoryAsync(Category category)
        {
            _context.Categories.Add(category);
            return await _context.SaveChangesAsync() > 0;
        }
    } 
}