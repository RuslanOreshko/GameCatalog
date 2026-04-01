using GameCatalog.ViewModels.Comments;

namespace GameCatalog.ViewModels.Games;

public class GameDetailsViewModel
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string GenreName { get; set; } = default!;
    public string Developer { get; set; } = default!;
    public string Publisher { get; set; } = default!;
    public DateTime ReleaseDate { get; set; }
    public string? CoverImageUrl { get; set; }
    public decimal Rating { get; set; }

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

    public List<CommentItemViewModel> Comments { get; set; } = new();
    public CreateCommentViewModel NewComment { get; set; } = new();
}