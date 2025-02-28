using System;
using System.Collections.Generic;

namespace PizzaShop.Data.Models;

public partial class Favouriteitem
{
    public int Favouriteitemid { get; set; }

    public int Itemid { get; set; }

    public int Userid { get; set; }

    public DateTime Createdat { get; set; }

    public DateTime Updatedat { get; set; }

    public int Createdby { get; set; }

    public int? Updatedby { get; set; }

    public bool Isdeleted { get; set; }

    public virtual Menuitem Item { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
