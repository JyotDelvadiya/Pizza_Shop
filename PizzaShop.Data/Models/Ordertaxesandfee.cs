using System;
using System.Collections.Generic;

namespace PizzaShop.Data.Models;

public partial class Ordertaxesandfee
{
    public int Id { get; set; }

    public int Taxid { get; set; }

    public int Orderid { get; set; }

    public DateTime Createdat { get; set; }

    public DateTime? Updatedat { get; set; }

    public int Createdby { get; set; }

    public int? Updatedby { get; set; }

    public virtual Orderheader Order { get; set; } = null!;

    public virtual Taxis Tax { get; set; } = null!;
}
