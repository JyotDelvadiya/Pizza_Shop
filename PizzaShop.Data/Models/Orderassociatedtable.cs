using System;
using System.Collections.Generic;

namespace PizzaShop.Data.Models;

public partial class Orderassociatedtable
{
    public int Id { get; set; }

    public int Orderid { get; set; }

    public int Tableid { get; set; }

    public string? Specialrequest { get; set; }

    public DateTime Createdat { get; set; }

    public DateTime? Updatedat { get; set; }

    public int Createdby { get; set; }

    public int? Updatedby { get; set; }

    public virtual Orderheader Order { get; set; } = null!;

    public virtual Table Table { get; set; } = null!;
}
