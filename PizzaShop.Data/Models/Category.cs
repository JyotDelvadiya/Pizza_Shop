using System;
using System.Collections.Generic;

namespace PizzaShop.Data.Models;

public partial class Category
{
    public int Categoryid { get; set; }

    public string Categoryname { get; set; } = null!;

    public string? Categorydescription { get; set; }

    public DateTime Createdat { get; set; }

    public DateTime? Updatedat { get; set; }

    public int Createdby { get; set; }

    public int? Updatedby { get; set; }

    public bool Isdeleted { get; set; }

    public virtual ICollection<Menuitem> Menuitems { get; } = new List<Menuitem>();
}
