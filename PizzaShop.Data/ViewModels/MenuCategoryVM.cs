using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PizzaShop.Data.Models;

namespace PizzaShop.Data.ViewModel
{
    public class MenuCategoryVM
    {
        public int categoryId { get; set; }
        
        [Required(ErrorMessage = "Category name is required")]
        [Display(Name = "Category Name")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Category name must be between 2 and 50 characters")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Category name should contain only letters")]
        public string Categoryname { get; set; }
        public string? Categorydescription { get; set; }
        public List<Category> Categories { get; set; } = new List<Category>();
        public List<Menuitem> Menuitems { get; set; } = new List<Menuitem>();
    }
}