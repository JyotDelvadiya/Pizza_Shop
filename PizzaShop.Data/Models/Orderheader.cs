using System;
using System.Collections.Generic;

namespace PizzaShop.Data.Models;

public partial class Orderheader
{
    public int Orderid { get; set; }

    public bool? Orderheaderstatus { get; set; }

    public string? Ordercomment { get; set; }

    public int? Orderrating { get; set; }

    public DateTime Createdat { get; set; }

    public DateTime? Updatedat { get; set; }

    public int Createdby { get; set; }

    public int? Updatedby { get; set; }

    public int Numberofpersons { get; set; }

    public string Customerphonenumber { get; set; } = null!;

    public string? Customername { get; set; }

    public virtual ICollection<Orderassociatedtable> Orderassociatedtables { get; } = new List<Orderassociatedtable>();

    public virtual ICollection<Orderdetail> Orderdetails { get; } = new List<Orderdetail>();

    public virtual ICollection<Ordertaxesandfee> Ordertaxesandfees { get; } = new List<Ordertaxesandfee>();

    public virtual ICollection<Payment> Payments { get; } = new List<Payment>();
}
