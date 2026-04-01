using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameCatalog.ViewModels.Games;

public class CreateGameViewModel
{
    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = default!;

    [Required]
    [MaxLength(5000)]
    public string Description { get; set; } = default!;

    [Required]
    public DateTime ReleaseDate { get; set; }

    [Required]
    [MaxLength(150)]
    public string Developer { get; set; } = default!;

    [Required]
    [MaxLength(150)]
    public string Publisher { get; set; } = default!;

    [MaxLength(500)]
    public string? CoverImageUrl { get; set; }

    [Range(0, 10)]
    public decimal Rating { get; set; }

    [Required]
    public int GenreId { get; set; }

    public string? MinimumOS { get; set; }
    public string? MinimumProcessor { get; set; }
    public string? MinimumRAM { get; set; }
    public string? MinimumGraphics { get; set; }
    public string? MinimumStorage { get; set; }

    public string? RecommendedOS { get; set; }
    public string? RecommendedProcessor { get; set; }
    public string? RecommendedRAM { get; set; }
    public string? RecommendedGraphics { get; set; }
    public string? RecommendedStorage { get; set; }

    public List<SelectListItem> Genres { get; set; } = new();
}