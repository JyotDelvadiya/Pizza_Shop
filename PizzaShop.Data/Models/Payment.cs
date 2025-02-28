using System;
using System.Collections.Generic;

namespace PizzaShop.Data.Models;

public partial class Payment
{
    public int Paymentid { get; set; }

    public int Orderid { get; set; }

    public decimal Ordertotalamount { get; set; }

    public string? Paymentmethod { get; set; }

    public string? Qrcode { get; set; }

    public int Customerid { get; set; }

    public DateTime Createdat { get; set; }

    public DateTime? Updatedat { get; set; }

    public int Createdby { get; set; }

    public int? Updatedby { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual Orderheader Order { get; set; } = null!;
}
