using System.Collections.Generic;
using PizzaShop.Data.Models;
public class PermissionItemVM
{
    public int PermissionId { get; set; }
    public string PermissionName { get; set; }
    public bool CanView { get; set; }
    public bool CanAddUpdate { get; set; }
    public bool CanDelete { get; set; }
    public bool IsSelected { get; set; } // For the checkbox on the left
}