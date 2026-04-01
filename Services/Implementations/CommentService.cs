using GameCatalog.Data;
using GameCatalog.Models.Entities;
using GameCatalog.Services.Interfaces;
using GameCatalog.ViewModels.Comments;
using Microsoft.EntityFrameworkCore;

namespace GameCatalog.Services.Implementations;

public class CommentService : ICommentService
{
    private readonly AppDbContext _context;

    public CommentService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<CommentItemViewModel>> GetByGameIdAsync(int gameId)
    {
        return await _context.Comments
            .AsNoTracking()
            .Where(c => c.GameId == gameId)
            .Include(c => c.User)
            .OrderByDescending(c => c.CreatedAt)
            .Select(c => new CommentItemViewModel
            {
                Id = c.Id,
                Content = c.Content,
                Username = c.User.Username,
                CreatedAt = c.CreatedAt
            })
            .ToListAsync();
    }

    public async Task AddAsync(CreateCommentViewModel model, int userId)
    {
        var gameExists = await _context.Games.AnyAsync(g => g.Id == model.GameId);
        if (!gameExists)
        {
            throw new InvalidOperationException("Game not found.");
        }

        var userExists = await _context.Users.AnyAsync(u => u.Id == userId);
        if (!userExists)
        {
            throw new InvalidOperationException("User not found.");
        }

        var comment = new Comment
        {
            Content = model.Content,
            GameId = model.GameId,
            UserId = userId,
            CreatedAt = DateTime.UtcNow
        };

        _context.Comments.Add(comment);
        await _context.SaveChangesAsync();
    }

    public async Task<List<CommentAdminListItemViewModel>> GetAllForAdminAsync()
    {
        return await _context.Comments
            .AsNoTracking()
            .Include(c => c.Game)
            .Include(c => c.User)
            .OrderByDescending(c => c.CreatedAt)
            .Select(c => new CommentAdminListItemViewModel
            {
                Id = c.Id,
                GameTitle = c.Game.Title,
                Username = c.User.Username,
                Content = c.Content,
                CreatedAt = c.CreatedAt
            })
            .ToListAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var comment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);
        if (comment is null)
        {
            return;
        }

        _context.Comments.Remove(comment);
        await _context.SaveChangesAsync();
    }
}