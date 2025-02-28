using System;
using System.Collections.Generic;

namespace PizzaShop.Data.Models;

public partial class Section
{
    public int Sectionid { get; set; }

    public string? Sectionname { get; set; }

    public string? Sectiondescription { get; set; }

    public DateTime Createdat { get; set; }

    public int Createdby { get; set; }

    public DateTime? Updatedat { get; set; }

    public int? Updatedby { get; set; }

    public bool Isdeleted { get; set; }

    public virtual ICollection<Table> Tables { get; } = new List<Table>();

    public virtual ICollection<Waitingtoken> Waitingtokens { get; } = new List<Waitingtoken>();
}
