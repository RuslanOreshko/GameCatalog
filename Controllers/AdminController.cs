using GameCatalog.Services.Interfaces;
using GameCatalog.ViewModels.Games;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GameCatalog.ViewModels.Genres;

namespace GameCatalog.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly IGameService _gameService;
    private readonly IGenreService _genreService;
    private readonly ICommentService _commentService;

    public AdminController(
        IGameService gameService,
        IGenreService genreService,
        ICommentService commentService)
    {
        _gameService = gameService;
        _genreService = genreService;
        _commentService = commentService;
    }

    public IActionResult Dashboard()
    {
        return View();
    }

    public async Task<IActionResult> Games()
    {
        var games = await _gameService.GetAllForAdminAsync();
        return View(games);
    }

    [HttpGet]
    public async Task<IActionResult> CreateGame()
    {
        var model = new CreateGameViewModel
        {
            Genres = await _genreService.GetSelectListAsync()
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateGame(CreateGameViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.Genres = await _genreService.GetSelectListAsync();
            return View(model);
        }

        await _gameService.CreateAsync(model);
        return RedirectToAction(nameof(Games));
    }

    [HttpGet]
    public async Task<IActionResult> EditGame(int id)
    {
        var model = await _gameService.GetEditByIdAsync(id);
        if (model is null)
        {
            return NotFound();
        }

        model.Genres = await _genreService.GetSelectListAsync();
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditGame(EditGameViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.Genres = await _genreService.GetSelectListAsync();
            return View(model);
        }

        await _gameService.UpdateAsync(model);
        return RedirectToAction(nameof(Games));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteGame(int id)
    {
        await _gameService.DeleteAsync(id);
        return RedirectToAction(nameof(Games));
    }

    public async Task<IActionResult> Genres()
    {
        var genres = await _genreService.GetAllForAdminAsync();
        return View(genres);
    }

    [HttpGet]
    public IActionResult CreateGenre()
    {
        return View(new CreateGenreViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateGenre(CreateGenreViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        await _genreService.CreateAsync(model);
        return RedirectToAction(nameof(Genres));
    }

    [HttpGet]
    public async Task<IActionResult> EditGenre(int id)
    {
        var model = await _genreService.GetEditByIdAsync(id);
        if (model is null)
        {
            return NotFound();
        }

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditGenre(EditGenreViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        await _genreService.UpdateAsync(model);
        return RedirectToAction(nameof(Genres));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteGenre(int id)
    {
        try
        {
            await _genreService.DeleteAsync(id);
        }
        catch (InvalidOperationException ex)
        {
            TempData["GenreError"] = ex.Message;
        }

        return RedirectToAction(nameof(Genres));
    }

    public async Task<IActionResult> Comments()
    {
        var comments = await _commentService.GetAllForAdminAsync();
        return View(comments);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteComment(int id)
    {
        await _commentService.DeleteAsync(id);
        return RedirectToAction(nameof(Comments));
    }
}