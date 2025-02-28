using System;
using System.Collections.Generic;

namespace PizzaShop.Data.Models;

public partial class Taxis
{
    public int Taxid { get; set; }

    public string? Taxname { get; set; }

    public string? Taxtype { get; set; }

    public decimal Taxamount { get; set; }

    public bool? Taxisdefault { get; set; }

    public bool? Isenabled { get; set; }

    public DateTime Createdat { get; set; }

    public DateTime? Updatedat { get; set; }

    public int Createdby { get; set; }

    public int? Updatedby { get; set; }

    public bool? Isdeleted { get; set; }

    public virtual ICollection<Ordertaxesandfee> Ordertaxesandfees { get; } = new List<Ordertaxesandfee>();
}
