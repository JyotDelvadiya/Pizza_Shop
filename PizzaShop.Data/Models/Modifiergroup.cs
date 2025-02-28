using System;
using System.Collections.Generic;

namespace PizzaShop.Data.Models;

public partial class Modifiergroup
{
    public int Modifiergroupid { get; set; }

    public string Modifiergroupname { get; set; } = null!;

    public string? Modifiergroupdescription { get; set; }

    public DateTime Createdat { get; set; }

    public DateTime? Updatedat { get; set; }

    public int Createdby { get; set; }

    public int? Updatedby { get; set; }

    public bool Isdeleted { get; set; }

    public virtual ICollection<Itemassociatemodifier> Itemassociatemodifiers { get; } = new List<Itemassociatemodifier>();

    public virtual ICollection<Modifieritem> Modifieritems { get; } = new List<Modifieritem>();
}
