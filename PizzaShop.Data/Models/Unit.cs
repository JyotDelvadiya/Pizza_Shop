using System;
using System.Collections.Generic;

namespace PizzaShop.Data.Models;

public partial class Unit
{
    public int Unitid { get; set; }

    public string Unitname { get; set; } = null!;

    public bool Isdeleted { get; set; }

    public DateTime Createdat { get; set; }

    public DateTime? Updatedat { get; set; }

    public int Createdby { get; set; }

    public int? Updatedby { get; set; }

    public virtual ICollection<Menuitem> Menuitems { get; } = new List<Menuitem>();

    public virtual ICollection<Modifieritem> Modifieritems { get; } = new List<Modifieritem>();
}
