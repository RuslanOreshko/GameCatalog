using System.Security.Cryptography;
using System.Text;
using GameCatalog.Data;
using GameCatalog.Models.Entities;
using GameCatalog.Services.Interfaces;
using GameCatalog.ViewModels.Account;
using Microsoft.EntityFrameworkCore;

namespace GameCatalog.Services.Implementations;

public class UserService : IUserService
{
    private readonly AppDbContext _context;

    public UserService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<(bool Success, string ErrorMessage)> RegisterAsync(RegisterViewModel model)
    {
        var usernameExists = await _context.Users.AnyAsync(u => u.Username == model.Username);
        if (usernameExists)
        {
            return (false, "Username is already taken.");
        }

        var emailExists = await _context.Users.AnyAsync(u => u.Email == model.Email);
        if (emailExists)
        {
            return (false, "Email is already taken.");
        }

        var user = new User
        {
            Username = model.Username,
            Email = model.Email,
            PasswordHash = HashPassword(model.Password),
            Role = "User"
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return (true, string.Empty);
    }

    public async Task<User?> ValidateUserAsync(LoginViewModel model)
    {
        var hashedPassword = HashPassword(model.Password);

        return await _context.Users.FirstOrDefaultAsync(u =>
            (u.Username == model.UsernameOrEmail || u.Email == model.UsernameOrEmail) &&
            u.PasswordHash == hashedPassword);
    }

    public async Task<ProfileViewModel?> GetProfileAsync(int userId)
    {
        return await _context.Users
            .AsNoTracking()
            .Where(u => u.Id == userId)
            .Select(u => new ProfileViewModel
            {
                Username = u.Username,
                Email = u.Email,
                Role = u.Role
            })
            .FirstOrDefaultAsync();
    }

    private static string HashPassword(string password)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(bytes);
    }
}