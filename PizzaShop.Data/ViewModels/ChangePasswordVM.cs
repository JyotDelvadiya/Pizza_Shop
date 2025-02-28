using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PizzaShop.Data.ViewModel
{
    public partial class ChangePasswordVM
    {
        [Required(ErrorMessage = "Current Password is required")]
        public string? CurrentPassword { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters long")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$", 
            ErrorMessage = "Password must include at least one uppercase letter, one lowercase letter, one number, and one special character")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string? NewPassword { get; set; }

        [Required(ErrorMessage = "Confirm New Password is required")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string? ConfirmNewPassword { get; set; }
    }
}