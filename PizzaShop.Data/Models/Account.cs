using System;
using System.Collections.Generic;

namespace PizzaShop.Data.Models;

public partial class Account
{
    public int Accountid { get; set; }

    public bool Isrememberme { get; set; }

    public DateTime? Createdat { get; set; }

    public DateTime? Updatedat { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Username { get; set; } = null!;

    public bool Isdeleted { get; set; }

    public virtual ICollection<User> Users { get; } = new List<User>();
}
