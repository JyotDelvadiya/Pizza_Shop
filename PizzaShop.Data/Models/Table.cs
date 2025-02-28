using System;
using System.Collections.Generic;

namespace PizzaShop.Data.Models;

public partial class Table
{
    public int Tableid { get; set; }

    public int Sectionid { get; set; }

    public int Capacity { get; set; }

    public string Tablenumber { get; set; } = null!;

    public bool? Status { get; set; }

    public DateTime Createdat { get; set; }

    public DateTime? Updatedat { get; set; }

    public int Createdby { get; set; }

    public int? Updatedby { get; set; }

    public bool Isdeleted { get; set; }

    public virtual ICollection<Orderassociatedtable> Orderassociatedtables { get; } = new List<Orderassociatedtable>();

    public virtual Section Section { get; set; } = null!;
}
