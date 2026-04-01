namespace GameCatalog.ViewModels.Genres;

public class GenreAdminListItemViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public int GamesCount { get; set; }
}