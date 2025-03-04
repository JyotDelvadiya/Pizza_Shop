using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using PizzaShop.Data.Models;

namespace PizzaShop.Data.ViewModel
{
    public partial class MyProfileVM
    {
        [Required(ErrorMessage = "First name is required")]
        [Display(Name = "First Name")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "First name must be between 2 and 50 characters")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "First name should contain only letters")]
        [Column("firstname")]
        public string Firstname { get; set; } = null!;

        [Required(ErrorMessage = "Last name is required")]
        [Display(Name = "Last Name")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Last name must be between 2 and 50 characters")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Last name should contain only letters")]
        [Column("lastname")]
        public string Lastname { get; set; } = null!;

        [Required(ErrorMessage = "Username is required")]
        [Display(Name = "Username")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Username must be between 5 and 30 characters")]
        [RegularExpression(@"^[a-zA-Z0-9_]+$", ErrorMessage = "Username can only contain letters, numbers, and underscores")]
        [Column("username")]
        public string Username { get; set; } = null!;

        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression(@"^[0-9]{10,15}$", ErrorMessage = "Please enter a valid Mobile number")]
        [Display(Name = "Phone Number")]
        [Column("phonenumber")]
        public string? Phonenumber { get; set; }
        
        [Column("cityid")]
        [StringLength(100)]
        public int? Cityid { get; set; }

        [Column("stateid")]
        [StringLength(100)]
        public int? Stateid { get; set; }

        [Required(ErrorMessage = "Please select a city")]
        [Display(Name = "City")]
        [Column("city")]
        public string? City { get; set; }

        [Required(ErrorMessage = "Please select a state")]
        [Display(Name = "State")]
        [Column("state")]
        public string? State { get; set; }

        [Required(ErrorMessage = "Please select a country")]
        [Display(Name = "Country")]
        [Column("country")]
        public string Country { get; set; } = null!;

        public List<Country>? Countries { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [StringLength(255, MinimumLength = 5, ErrorMessage = "Address must be between 5 and 255 characters")]
        [Display(Name = "Address")]
        [Column("address")]
        public string? Address { get; set; }

        [Required(ErrorMessage = "Zip code is required")]
        [RegularExpression(@"^\d{6}$", ErrorMessage = "Please enter a valid 6-digit zip code (e.g., 361002)")]
        [Display(Name = "Zip Code")]
        [Column("zipcode")]
        public string? Zipcode { get; set; }

        [Column("RoleName")]
        public string? RoleName { get; set; }

        [Column("profileimage")]
        public string? Profileimage { get; set; }

        [Column("accountid")]
        public int Accountid { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        [Display(Name = "Email Address")]
        public string Email { get; set; } = null!;
    }
}