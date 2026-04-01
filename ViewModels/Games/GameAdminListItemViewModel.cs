namespace GameCatalog.ViewModels.Games;

public class GameAdminListItemViewModel
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
    public string GenreName { get; set; } = default!;
    public string Developer { get; set; } = default!;
    public DateTime ReleaseDate { get; set; }
    public decimal Rating { get; set; }
}