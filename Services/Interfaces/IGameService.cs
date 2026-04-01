using GameCatalog.ViewModels.Games;

namespace GameCatalog.Services.Interfaces;

public interface IGameService
{
    Task<List<GameListItemViewModel>> GetAllAsync();
    Task<GameDetailsViewModel?> GetByIdAsync(int id);

    Task<List<GameAdminListItemViewModel>> GetAllForAdminAsync();
    Task CreateAsync(CreateGameViewModel model);
    Task<EditGameViewModel?> GetEditByIdAsync(int id);
    Task UpdateAsync(EditGameViewModel model);
    Task DeleteAsync(int id);
}