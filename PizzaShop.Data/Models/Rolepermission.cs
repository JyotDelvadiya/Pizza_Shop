﻿using System;
using System.Collections.Generic;

namespace PizzaShop.Data.Models;

public partial class Rolepermission
{
    public int Id { get; set; }

    public int Roleid { get; set; }

    public int Permissionid { get; set; }

    public bool? Canview { get; set; }

    public bool? Canaddupdate { get; set; }

    public bool? Candelete { get; set; }

    public virtual Permission Permission { get; set; } = null!;

    public virtual Role Role { get; set; } = null!;
}
