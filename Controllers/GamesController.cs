using GameCatalog.Services.Interfaces;
using GameCatalog.ViewModels.Comments;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace GameCatalog.Controllers;

public class GamesController : Controller
{
    private readonly IGameService _gameService;
    private readonly ICommentService _commentService;

    public GamesController(IGameService gameService, ICommentService commentService)
    {
        _gameService = gameService;
        _commentService = commentService;
    }

    public async Task<IActionResult> Index()
    {
        var games = await _gameService.GetAllAsync();
        return View(games);
    }

    public async Task<IActionResult> Details(int id)
    {
        var game = await _gameService.GetByIdAsync(id);

        if (game is null)
        {
            return NotFound();
        }

        return View(game);
    }

    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddComment(CreateCommentViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return RedirectToAction(nameof(Details), new { id = model.GameId });
        }

        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (!int.TryParse(userIdClaim, out var userId))
        {
            return Unauthorized();
        }

        await _commentService.AddAsync(model, userId);

        return RedirectToAction(nameof(Details), new { id = model.GameId });
    }
}