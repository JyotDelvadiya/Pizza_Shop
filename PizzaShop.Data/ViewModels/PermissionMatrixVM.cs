using System.Collections.Generic;
using PizzaShop.Data.Models;

namespace PizzaShop.Data.ViewModel
{
    public class PermissionMatrixVM
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public List<PermissionItemVM> Permissions { get; set; } = new List<PermissionItemVM>();
    }
}