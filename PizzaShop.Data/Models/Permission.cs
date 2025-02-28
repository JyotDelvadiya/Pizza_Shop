using System;
using System.Collections.Generic;

namespace PizzaShop.Data.Models;

public partial class Permission
{
    public int Permissionid { get; set; }

    public string Permissionname { get; set; } = null!;

    public DateTime? Updatedat { get; set; }

    public int? Updatedby { get; set; }

    public virtual ICollection<Rolepermission> Rolepermissions { get; } = new List<Rolepermission>();
}
