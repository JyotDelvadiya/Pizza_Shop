using System;
using System.Collections.Generic;

namespace PizzaShop.Data.Models;

public partial class Menuitem
{
    public int Menuitemid { get; set; }

    public int Categoryid { get; set; }

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

    public bool Isdeleted { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<Favouriteitem> Favouriteitems { get; } = new List<Favouriteitem>();

    public virtual ICollection<Itemassociatemodifier> Itemassociatemodifiers { get; } = new List<Itemassociatemodifier>();

    public virtual ICollection<Orderdetail> Orderdetails { get; } = new List<Orderdetail>();

    public virtual Unit Unit { get; set; } = null!;
}
