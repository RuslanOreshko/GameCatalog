using System.ComponentModel.DataAnnotations;

namespace GameCatalog.ViewModels.Comments;

public class CreateCommentViewModel
{
    public int GameId { get; set; }

    [Required]
    [MaxLength(2000)]
    public string Content { get; set; } = default!;
}