using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PizzaShop.Data.ViewModel
{
    public partial class RolesVM
    {
        public int Roleid { get; set; }

        public string Rolename { get; set; } = null!;
    }
}