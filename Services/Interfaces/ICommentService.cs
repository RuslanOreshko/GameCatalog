using GameCatalog.ViewModels.Comments;

namespace GameCatalog.Services.Interfaces;

public interface ICommentService
{
    Task<List<CommentItemViewModel>> GetByGameIdAsync(int gameId);
    Task AddAsync(CreateCommentViewModel model, int userId);

    Task<List<CommentAdminListItemViewModel>> GetAllForAdminAsync();
    Task DeleteAsync(int id);
}