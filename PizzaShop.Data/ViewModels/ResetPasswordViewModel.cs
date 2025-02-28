using System.ComponentModel.DataAnnotations;

public class ResetPasswordViewModel
{
    [Required]
    [EmailAddress]
    public string? Email { get; set; }

    [Required]
    public string? Token { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 6)]
    [DataType(DataType.Password)]
    public string? NewPassword { get; set; }

    [DataType(DataType.Password)]
    [Compare("NewPassword", ErrorMessage = "The password and confirmation password do not match.")]
    public string? ConfirmPassword { get; set; }
}