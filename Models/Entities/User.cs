using System.ComponentModel.DataAnnotations;

namespace GameCatalog.Models.Entities;

public class User
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Username { get; set; } = default!;

    [Required]
    [MaxLength(150)]
    public string Email { get; set; } = default!;

    [Required]
    public string PasswordHash { get; set; } = default!;

    [Required]
    [MaxLength(20)]
    public string Role { get; set; } = "User";

    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
}