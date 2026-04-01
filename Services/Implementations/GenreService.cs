using GameCatalog.Data;
using GameCatalog.Models.Entities;
using GameCatalog.Services.Interfaces;
using GameCatalog.ViewModels.Genres;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GameCatalog.Services.Implementations;

public class GenreService : IGenreService
{
    private readonly AppDbContext _context;

    public GenreService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<SelectListItem>> GetSelectListAsync()
    {
        return await _context.Genres
            .AsNoTracking()
            .OrderBy(g => g.Name)
            .Select(g => new SelectListItem
            {
                Value = g.Id.ToString(),
                Text = g.Name
            })
            .ToListAsync();
    }

    public async Task<List<GenreAdminListItemViewModel>> GetAllForAdminAsync()
    {
        return await _context.Genres
            .AsNoTracking()
            .OrderBy(g => g.Name)
            .Select(g => new GenreAdminListItemViewModel
            {
                Id = g.Id,
                Name = g.Name,
                Description = g.Description,
                GamesCount = g.Games.Count
            })
            .ToListAsync();
    }

    public async Task CreateAsync(CreateGenreViewModel model)
    {
        var genre = new Genre
        {
            Name = model.Name,
            Description = model.Description
        };

        _context.Genres.Add(genre);
        await _context.SaveChangesAsync();
    }

    public async Task<EditGenreViewModel?> GetEditByIdAsync(int id)
    {
        return await _context.Genres
            .AsNoTracking()
            .Where(g => g.Id == id)
            .Select(g => new EditGenreViewModel
            {
                Id = g.Id,
                Name = g.Name,
                Description = g.Description
            })
            .FirstOrDefaultAsync();
    }

    public async Task UpdateAsync(EditGenreViewModel model)
    {
        var genre = await _context.Genres.FirstOrDefaultAsync(g => g.Id == model.Id);
        if (genre is null)
        {
            throw new InvalidOperationException("Genre not found.");
        }

        genre.Name = model.Name;
        genre.Description = model.Description;

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var genre = await _context.Genres
            .Include(g => g.Games)
            .FirstOrDefaultAsync(g => g.Id == id);

        if (genre is null)
        {
            return;
        }

        if (genre.Games.Any())
        {
            throw new InvalidOperationException("Cannot delete genre that has games.");
        }

        _context.Genres.Remove(genre);
        await _context.SaveChangesAsync();
    }
}