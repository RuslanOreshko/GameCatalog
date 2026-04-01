using GameCatalog.ViewModels.Genres;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameCatalog.Services.Interfaces;

public interface IGenreService
{
    Task<List<SelectListItem>> GetSelectListAsync();

    Task<List<GenreAdminListItemViewModel>> GetAllForAdminAsync();
    Task CreateAsync(CreateGenreViewModel model);
    Task<EditGenreViewModel?> GetEditByIdAsync(int id);
    Task UpdateAsync(EditGenreViewModel model);
    Task DeleteAsync(int id);
}