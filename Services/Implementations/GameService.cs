using GameCatalog.Data;
using GameCatalog.Services.Interfaces;
using GameCatalog.ViewModels.Games;
using Microsoft.EntityFrameworkCore;

using GameCatalog.ViewModels.Comments;

namespace GameCatalog.Services.Implementations;

public class GameService : IGameService
{
    private readonly AppDbContext _context;

    public GameService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<GameListItemViewModel>> GetAllAsync()
    {
        return await _context.Games
            .AsNoTracking()
            .Include(g => g.Genre)
            .OrderBy(g => g.Title)
            .Select(g => new GameListItemViewModel
            {
                Id = g.Id,
                Title = g.Title,
                GenreName = g.Genre.Name,
                CoverImageUrl = g.CoverImageUrl,
                Developer = g.Developer,
                ReleaseDate = g.ReleaseDate,
                Rating = g.Rating
            })
            .ToListAsync();
    }

    public async Task<GameDetailsViewModel?> GetByIdAsync(int id)
    {
        return await _context.Games
            .AsNoTracking()
            .Include(g => g.Genre)
            .Include(g => g.Comments)
                .ThenInclude(c => c.User)
            .Where(g => g.Id == id)
            .Select(g => new GameDetailsViewModel
            {
                Id = g.Id,
                Title = g.Title,
                Description = g.Description,
                GenreName = g.Genre.Name,
                Developer = g.Developer,
                Publisher = g.Publisher,
                ReleaseDate = g.ReleaseDate,
                CoverImageUrl = g.CoverImageUrl,
                Rating = g.Rating,

                MinimumOS = g.MinimumOS,
                MinimumProcessor = g.MinimumProcessor,
                MinimumRAM = g.MinimumRAM,
                MinimumGraphics = g.MinimumGraphics,
                MinimumStorage = g.MinimumStorage,

                RecommendedOS = g.RecommendedOS,
                RecommendedProcessor = g.RecommendedProcessor,
                RecommendedRAM = g.RecommendedRAM,
                RecommendedGraphics = g.RecommendedGraphics,
                RecommendedStorage = g.RecommendedStorage,

                Comments = g.Comments
                    .OrderByDescending(c => c.CreatedAt)
                    .Select(c => new CommentItemViewModel
                    {
                        Id = c.Id,
                        Content = c.Content,
                        Username = c.User.Username,
                        CreatedAt = c.CreatedAt
                    })
                    .ToList(),

                NewComment = new CreateCommentViewModel
                {
                    GameId = g.Id
                }
            })
            .FirstOrDefaultAsync();
    }

    public async Task<List<GameAdminListItemViewModel>> GetAllForAdminAsync()
    {
        return await _context.Games
            .AsNoTracking()
            .Include(g => g.Genre)
            .OrderBy(g => g.Title)
            .Select(g => new GameAdminListItemViewModel
            {
                Id = g.Id,
                Title = g.Title,
                GenreName = g.Genre.Name,
                Developer = g.Developer,
                ReleaseDate = g.ReleaseDate,
                Rating = g.Rating
            })
            .ToListAsync();
    }

    public async Task CreateAsync(CreateGameViewModel model)
    {
        var game = new Models.Entities.Game
        {
            Title = model.Title,
            Description = model.Description,
            ReleaseDate = DateTime.SpecifyKind(model.ReleaseDate, DateTimeKind.Utc),
            Developer = model.Developer,
            Publisher = model.Publisher,
            CoverImageUrl = model.CoverImageUrl,
            Rating = model.Rating,
            GenreId = model.GenreId,

            MinimumOS = model.MinimumOS,
            MinimumProcessor = model.MinimumProcessor,
            MinimumRAM = model.MinimumRAM,
            MinimumGraphics = model.MinimumGraphics,
            MinimumStorage = model.MinimumStorage,

            RecommendedOS = model.RecommendedOS,
            RecommendedProcessor = model.RecommendedProcessor,
            RecommendedRAM = model.RecommendedRAM,
            RecommendedGraphics = model.RecommendedGraphics,
            RecommendedStorage = model.RecommendedStorage
        };

        _context.Games.Add(game);
        await _context.SaveChangesAsync();
    }

    public async Task<EditGameViewModel?> GetEditByIdAsync(int id)
    {
        return await _context.Games
            .AsNoTracking()
            .Where(g => g.Id == id)
            .Select(g => new EditGameViewModel
            {
                Id = g.Id,
                Title = g.Title,
                Description = g.Description,
                ReleaseDate = g.ReleaseDate,
                Developer = g.Developer,
                Publisher = g.Publisher,
                CoverImageUrl = g.CoverImageUrl,
                Rating = g.Rating,
                GenreId = g.GenreId,

                MinimumOS = g.MinimumOS,
                MinimumProcessor = g.MinimumProcessor,
                MinimumRAM = g.MinimumRAM,
                MinimumGraphics = g.MinimumGraphics,
                MinimumStorage = g.MinimumStorage,

                RecommendedOS = g.RecommendedOS,
                RecommendedProcessor = g.RecommendedProcessor,
                RecommendedRAM = g.RecommendedRAM,
                RecommendedGraphics = g.RecommendedGraphics,
                RecommendedStorage = g.RecommendedStorage
            })
            .FirstOrDefaultAsync();
    }

    public async Task UpdateAsync(EditGameViewModel model)
    {
        var game = await _context.Games.FirstOrDefaultAsync(g => g.Id == model.Id);
        if (game is null)
        {
            throw new InvalidOperationException("Game not found.");
        }

        game.Title = model.Title;
        game.Description = model.Description;
        game.ReleaseDate = DateTime.SpecifyKind(model.ReleaseDate, DateTimeKind.Utc);
        game.Developer = model.Developer;
        game.Publisher = model.Publisher;
        game.CoverImageUrl = model.CoverImageUrl;
        game.Rating = model.Rating;
        game.GenreId = model.GenreId;

        game.MinimumOS = model.MinimumOS;
        game.MinimumProcessor = model.MinimumProcessor;
        game.MinimumRAM = model.MinimumRAM;
        game.MinimumGraphics = model.MinimumGraphics;
        game.MinimumStorage = model.MinimumStorage;

        game.RecommendedOS = model.RecommendedOS;
        game.RecommendedProcessor = model.RecommendedProcessor;
        game.RecommendedRAM = model.RecommendedRAM;
        game.RecommendedGraphics = model.RecommendedGraphics;
        game.RecommendedStorage = model.RecommendedStorage;

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var game = await _context.Games.FirstOrDefaultAsync(g => g.Id == id);
        if (game is null)
        {
            return;
        }

        _context.Games.Remove(game);
        await _context.SaveChangesAsync();
    }
}