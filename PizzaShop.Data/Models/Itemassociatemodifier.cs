using System;
using System.Collections.Generic;

namespace PizzaShop.Data.Models;

public partial class Itemassociatemodifier
{
    public int Itemassociatemodifierid { get; set; }

    public int Itemid { get; set; }

    public int Modifiergroupid { get; set; }

    public DateTime Createdat { get; set; }

    public DateTime? Updatedat { get; set; }

    public int Createdby { get; set; }

    public int? Updatedby { get; set; }

    public virtual Menuitem Item { get; set; } = null!;

    public virtual Modifiergroup Modifiergroup { get; set; } = null!;
}
