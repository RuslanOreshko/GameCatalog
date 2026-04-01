using System.ComponentModel.DataAnnotations;

namespace GameCatalog.Models.Entities;

public class Genre
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = default!;

    [MaxLength(1000)]
    public string? Description { get; set; }

    public ICollection<Game> Games { get; set; } = new List<Game>();
}