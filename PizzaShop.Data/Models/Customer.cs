using System;
using System.Collections.Generic;

namespace PizzaShop.Data.Models;

public partial class Customer
{
    public int Customerid { get; set; }

    public string? Customeremail { get; set; }

    public string? Customername { get; set; }

    public string Customerphonenumber { get; set; } = null!;

    public DateTime Createdat { get; set; }

    public DateTime? Updatedat { get; set; }

    public int Createdby { get; set; }

    public int? Updatedby { get; set; }

    public virtual ICollection<Payment> Payments { get; } = new List<Payment>();
}
