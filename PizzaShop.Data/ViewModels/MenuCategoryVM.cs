using System.Collections.Generic;
using PizzaShop.Data.Models;

namespace PizzaShop.Data.ViewModel
{
    public class MenuCategoryVM
    {
        public string Categoryname { get; set; }
        public string? Categorydescription { get; set; }
        public List<Category> Categories { get; set; } = new List<Category>();
        public List<Menuitem> Menuitems { get; set; } = new List<Menuitem>();
    }
}