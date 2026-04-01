using System.ComponentModel.DataAnnotations;

namespace GameCatalog.ViewModels.Account;

public class LoginViewModel
{
    [Required]
    public string UsernameOrEmail { get; set; } = default!;

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = default!;
}