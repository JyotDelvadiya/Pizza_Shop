using System;
using System.Collections.Generic;

namespace PizzaShop.Data.Models;

public partial class Role
{
    public int Roleid { get; set; }

    public string Rolename { get; set; } = null!;

    public virtual ICollection<Rolepermission> Rolepermissions { get; } = new List<Rolepermission>();

    public virtual ICollection<User> Users { get; } = new List<User>();
}
