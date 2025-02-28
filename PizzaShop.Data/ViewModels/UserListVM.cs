using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using PizzaShop.Data.Models;

namespace PizzaShop.Data.ViewModel
{
    public partial class UserListVM
    {
        public int Userid { get; set; }

        public bool Isdeleted { get; set; }

        [Column("profileimage")]
        public string? Profileimage { get; set; }

        [Column("firstname")]
        [StringLength(50)]
        public string Firstname { get; set; } = null!;

        [Column("lastname")]
        [StringLength(50)]
        public string Lastname { get; set; } = null!;

        public string Email { get; set; } = null!;

        [Column("phonenumber")]
        [StringLength(15)]
        public string? Phonenumber { get; set; }
        
        [Column("RoleName")]
        public string? RoleName { get; set; }

        public int Status { get; set; }
    }
}