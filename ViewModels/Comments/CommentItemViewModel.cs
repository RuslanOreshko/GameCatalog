namespace GameCatalog.ViewModels.Comments;

public class CommentItemViewModel
{
    public int Id { get; set; }
    public string Content { get; set; } = default!;
    public string Username { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
}