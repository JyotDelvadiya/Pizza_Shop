using System;
using System.Collections.Generic;

namespace PizzaShop.Data.Models;

public partial class User
{
    public int Userid { get; set; }

    public string Firstname { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public string? Phonenumber { get; set; }

    public int? City { get; set; }

    public int? State { get; set; }

    public int? Country { get; set; }

    public string? Address { get; set; }

    public string? Zipcode { get; set; }

    public int Roleid { get; set; }

    public int Status { get; set; }

    public string? Profileimage { get; set; }

    public DateTime Createdat { get; set; }

    public DateTime? Updatedat { get; set; }

    public int Createdby { get; set; }

    public int? Updatedby { get; set; }

    public int? Accountid { get; set; }

    public bool Isdeleted { get; set; }

    public virtual Account? Account { get; set; }

    public virtual ICollection<Favouriteitem> Favouriteitems { get; } = new List<Favouriteitem>();

    public virtual Role Role { get; set; } = null!;
}
