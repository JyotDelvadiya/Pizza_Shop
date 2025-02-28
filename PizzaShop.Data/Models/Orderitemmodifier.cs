using System;
using System.Collections.Generic;

namespace PizzaShop.Data.Models;

public partial class Orderitemmodifier
{
    public int Orderitemmodifierid { get; set; }

    public int Orderdetailid { get; set; }

    public int Modifieroptionid { get; set; }

    public DateTime Createdat { get; set; }

    public DateTime? Updatedat { get; set; }

    public int Createdby { get; set; }

    public int? Updatedby { get; set; }

    public int Itemmodifierquantity { get; set; }

    public virtual Modifieritem Modifieroption { get; set; } = null!;

    public virtual Orderdetail Orderdetail { get; set; } = null!;
}
