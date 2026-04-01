using System.ComponentModel.DataAnnotations;

namespace GameCatalog.Models.Entities;

public class Game
{
    public int Id { get; set; }

    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = default!;

    [Required]
    [MaxLength(5000)]
    public string Description { get; set; } = default!;

    public DateTime ReleaseDate { get; set; }

    [MaxLength(150)]
    public string Developer { get; set; } = default!;

    [MaxLength(150)]
    public string Publisher { get; set; } = default!;

    [MaxLength(500)]
    public string? CoverImageUrl { get; set; }

    public decimal Rating { get; set; }

    public int GenreId { get; set; }
    public Genre Genre { get; set; } = default!;

    [MaxLength(200)]
    public string? MinimumOS { get; set; }

    [MaxLength(200)]
    public string? MinimumProcessor { get; set; }

    [MaxLength(100)]
    public string? MinimumRAM { get; set; }

    [MaxLength(200)]
    public string? MinimumGraphics { get; set; }

    [MaxLength(100)]
    public string? MinimumStorage { get; set; }

    [MaxLength(200)]
    public string? RecommendedOS { get; set; }

    [MaxLength(200)]
    public string? RecommendedProcessor { get; set; }

    [MaxLength(100)]
    public string? RecommendedRAM { get; set; }

    [MaxLength(200)]
    public string? RecommendedGraphics { get; set; }

    [MaxLength(100)]
    public string? RecommendedStorage { get; set; }

    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
}