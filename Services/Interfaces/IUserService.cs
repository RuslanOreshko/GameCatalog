using GameCatalog.Models.Entities;
using GameCatalog.ViewModels.Account;

namespace GameCatalog.Services.Interfaces;

public interface IUserService
{
    Task<(bool Success, string ErrorMessage)> RegisterAsync(RegisterViewModel model);
    Task<User?> ValidateUserAsync(LoginViewModel model);
    Task<ProfileViewModel?> GetProfileAsync(int userId);
}