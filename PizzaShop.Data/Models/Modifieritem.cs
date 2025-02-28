using System;
using System.Collections.Generic;

namespace PizzaShop.Data.Models;

public partial class Modifieritem
{
    public int Modifieritemid { get; set; }

    public int Modifiergroupid { get; set; }

    public string Modifieritemname { get; set; } = null!;

    public decimal Modifierrate { get; set; }

    public int Modifierquantity { get; set; }

    public string? Modifieritemdescription { get; set; }

    public DateTime Createdat { get; set; }

    public DateTime? Updatedat { get; set; }

    public int Createdby { get; set; }

    public int? Updatedby { get; set; }

    public int Unitid { get; set; }

    public bool Isdeleted { get; set; }

    public virtual Modifiergroup Modifiergroup { get; set; } = null!;

    public virtual ICollection<Orderitemmodifier> Orderitemmodifiers { get; } = new List<Orderitemmodifier>();

    public virtual Unit Unit { get; set; } = null!;
}
