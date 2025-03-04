using System.Collections.Generic;
using PizzaShop.Data.Models;

namespace PizzaShop.Data.ViewModel
{
    public class MenuItemVM
    {
        public int Menuitemid { get; set; }

        public bool Itemtype { get; set; }

        public decimal Rate { get; set; }

        public bool? Status { get; set; }

        public string? Description { get; set; }

        public string? Itemimage { get; set; }

        public double? Taxpercentage { get; set; }

        public string Shortcode { get; set; } = null!;

        public int Defaulttax { get; set; }

        public DateTime Createdat { get; set; }

        public DateTime? Updatedat { get; set; }

        public int Createdby { get; set; }

        public int? Updatedby { get; set; }

        public DateTime? Deletedat { get; set; }

        public int? Deletedby { get; set; }

        public int Unitid { get; set; }
    }
}