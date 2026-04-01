namespace GameCatalog.ViewModels.Games;

public class GameListItemViewModel
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
    public string GenreName { get; set; } = default!;
    public string? CoverImageUrl { get; set; }
    public string Developer { get; set; } = default!;
    public DateTime ReleaseDate { get; set; }
    public decimal Rating { get; set; }
}