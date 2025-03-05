using Microsoft.EntityFrameworkCore;
using MimeKit.Tnef;
using Org.BouncyCastle.Asn1;
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
            var categories = await _context.Categories
                                            .Where(c => !c.Isdeleted)
                                            .OrderBy(c => c.Categoryid)
                                            .ToListAsync();
            foreach (var Category in categories)
            {
                yield return Category;
            }
        }
        public async IAsyncEnumerable<Menuitem> GetMenuItemsAsync(int categoryId)
        {
            var menuItems = await _context.Menuitems
                                            .Where(m => !m.Isdeleted && m.Categoryid == categoryId)
                                            .ToListAsync();
            foreach (var Menuitem in menuItems)
            {
                yield return Menuitem;
            }
        }
        public async Task<Category?> CheckCategoryExistanceAsync(Category category)
        {
            var existingCategory = await _context.Categories.FirstOrDefaultAsync(c => c.Categoryname == category.Categoryname && c.Isdeleted != true);
            return existingCategory;
        }
        public async Task AddCategoryAsync(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
        }
        public async Task<Category?> CheckCategoryExistanceExceptGivenAsync(Category category)
        {
            var existingCategory = await _context.Categories.FirstOrDefaultAsync(c => c.Categoryname == category.Categoryname && c.Categoryid != category.Categoryid);
            return existingCategory;
        }
        public async Task EditCategoryAsync(Category category)
        {
            var existingCategory = await _context.Categories
                .FirstOrDefaultAsync(c => c.Categoryid == category.Categoryid);

            if (existingCategory == null)
            {
                throw new ArgumentException("Category not found");
            }

            existingCategory.Categoryname = category.Categoryname;
            existingCategory.Categorydescription = category.Categorydescription;
            // existingCategory.Updatedat = DateTime.UtcNow;

            await _context.SaveChangesAsync();
        }
        public async Task<bool> DeleteCategoryAsync(int categoryId)
        {
            var categoryToDelete = await _context.Categories.FirstOrDefaultAsync(c => c.Categoryid == categoryId);
            if (categoryToDelete != null)
            {
                categoryToDelete.Isdeleted = true;
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}