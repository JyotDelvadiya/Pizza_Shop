using System;
using System.Collections.Generic;

namespace PizzaShop.Data.Models;

public partial class Waitingtoken
{
    public int Tokenid { get; set; }

    public string? Customeremail { get; set; }

    public string? Customername { get; set; }

    public string Customerphonenumber { get; set; } = null!;

    public int Numberofpersons { get; set; }

    public int Sectionid { get; set; }

    public string Tokennumber { get; set; } = null!;

    public DateTime? Createdat { get; set; }

    public DateTime? Updatedat { get; set; }

    public int? Createdby { get; set; }

    public int? Updatedby { get; set; }

    public bool Isassigned { get; set; }

    public bool Isdeleted { get; set; }

    public virtual Section Section { get; set; } = null!;
}
