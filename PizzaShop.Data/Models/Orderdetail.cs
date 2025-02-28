using System;
using System.Collections.Generic;

namespace PizzaShop.Data.Models;

public partial class Orderdetail
{
    public int Orderdetailsid { get; set; }

    public int Orderid { get; set; }

    public int Menuitemid { get; set; }

    public int Quantity { get; set; }

    public decimal Unitprice { get; set; }

    public string? Itemnote { get; set; }

    public int? Status { get; set; }

    public DateTime Createdat { get; set; }

    public DateTime? Updatedat { get; set; }

    public int Createdby { get; set; }

    public int? Updatedby { get; set; }

    public virtual Menuitem Menuitem { get; set; } = null!;

    public virtual Orderheader Order { get; set; } = null!;

    public virtual ICollection<Orderitemmodifier> Orderitemmodifiers { get; } = new List<Orderitemmodifier>();
}
