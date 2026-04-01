using System.ComponentModel.DataAnnotations;

namespace GameCatalog.ViewModels.Genres;

public class EditGenreViewModel
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = default!;

    [MaxLength(1000)]
    public string? Description { get; set; }
}