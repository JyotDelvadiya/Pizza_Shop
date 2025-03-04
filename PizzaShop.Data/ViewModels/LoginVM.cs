using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PizzaShop.Data.ViewModel
{
    public partial class LoginVM
    {
        [Column("isrememberme")]
        public bool Isrememberme { get; set; } = false;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [Column("email")]
        [StringLength(255)]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Password is required")]
        [Column("password")]
        [StringLength(255)]
        public string Password { get; set; } = null!;
    }
}