namespace GameCatalog.ViewModels.Comments;

public class CommentAdminListItemViewModel
{
    public int Id { get; set; }
    public string GameTitle { get; set; } = default!;
    public string Username { get; set; } = default!;
    public string Content { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
}