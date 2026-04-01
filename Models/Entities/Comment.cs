using System.ComponentModel.DataAnnotations;

namespace GameCatalog.Models.Entities;

public class Comment
{
    public int Id { get; set; }

    [Required]
    [MaxLength(2000)]
    public string Content { get; set; } = default!;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public int GameId { get; set; }
    public Game Game { get; set; } = default!;

    public int UserId { get; set; }
    public User User { get; set; } = default!;
}