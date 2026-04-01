using System.ComponentModel.DataAnnotations;

namespace GameCatalog.ViewModels.Account;

public class RegisterViewModel
{
    [Required]
    [MaxLength(100)]
    public string Username { get; set; } = default!;

    [Required]
    [EmailAddress]
    [MaxLength(150)]
    public string Email { get; set; } = default!;

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = default!;

    [Required]
    [DataType(DataType.Password)]
    [Compare(nameof(Password), ErrorMessage = "Passwords do not match.")]
    public string ConfirmPassword { get; set; } = default!;
}