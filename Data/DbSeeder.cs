using GameCatalog.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameCatalog.Data;

public static class DbSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        await context.Database.MigrateAsync();

        if (!await context.Genres.AnyAsync())
        {
            var genres = new List<Genre>
            {
                new Genre { Name = "Action", Description = "Fast-paced action games" },
                new Genre { Name = "RPG", Description = "Role-playing games" },
                new Genre { Name = "Shooter", Description = "First/third-person shooters" },
                new Genre { Name = "Strategy", Description = "Strategy and tactics games" },
                new Genre { Name = "Adventure", Description = "Story-driven adventure games" }
            };

            await context.Genres.AddRangeAsync(genres);
            await context.SaveChangesAsync();
        }

        if (!await context.Games.AnyAsync())
        {
            var genres = await context.Genres.ToListAsync();

            var games = new List<Game>
            {
                new Game
                {
                    Title = "Cyberpunk 2077",
                    CoverImageUrl = "wwwroot/images/Cyberpunk2077.jpg",
                    Description = "Open-world RPG set in Night City.",
                    ReleaseDate = DateTime.SpecifyKind(new DateTime(2020, 12, 10), DateTimeKind.Utc),
                    Developer = "CD Projekt Red",
                    Publisher = "CD Projekt",
                    GenreId = genres.First(g => g.Name == "RPG").Id,
                    Rating = 8.5m,

                    MinimumOS = "Windows 10",
                    MinimumProcessor = "Intel Core i5-3570K",
                    MinimumRAM = "8 GB",
                    MinimumGraphics = "GTX 780",
                    MinimumStorage = "70 GB",

                    RecommendedOS = "Windows 10",
                    RecommendedProcessor = "Intel Core i7-4790",
                    RecommendedRAM = "16 GB",
                    RecommendedGraphics = "RTX 2060",
                    RecommendedStorage = "70 GB"
                },

                new Game
                {
                    Title = "The Witcher 3",
                    CoverImageUrl = "https://images.igdb.com/igdb/image/upload/t_cover_big/co1wyy.jpg",
                    Description = "Story-driven RPG with open world.",
                    ReleaseDate = DateTime.SpecifyKind(new DateTime(2015, 5, 19), DateTimeKind.Utc),
                    Developer = "CD Projekt Red",
                    Publisher = "CD Projekt",
                    GenreId = genres.First(g => g.Name == "RPG").Id,
                    Rating = 9.5m,

                    MinimumOS = "Windows 7",
                    MinimumProcessor = "Intel Core i5-2500K",
                    MinimumRAM = "6 GB",
                    MinimumGraphics = "GTX 660",
                    MinimumStorage = "50 GB",

                    RecommendedOS = "Windows 10",
                    RecommendedProcessor = "Intel Core i7",
                    RecommendedRAM = "8 GB",
                    RecommendedGraphics = "GTX 970",
                    RecommendedStorage = "50 GB"
                },

                new Game
                {
                    Title = "Counter-Strike 2",
                    CoverImageUrl = "https://images.igdb.com/igdb/image/upload/t_cover_big/co5v8n.jpg",
                    Description = "Competitive online shooter.",
                    ReleaseDate = DateTime.SpecifyKind(new DateTime(2023, 9, 27), DateTimeKind.Utc),
                    Developer = "Valve",
                    Publisher = "Valve",
                    GenreId = genres.First(g => g.Name == "Shooter").Id,
                    Rating = 8.0m,

                    MinimumOS = "Windows 10",
                    MinimumProcessor = "Intel Core i5",
                    MinimumRAM = "8 GB",
                    MinimumGraphics = "GTX 1050",
                    MinimumStorage = "30 GB"
                },

                new Game
                {
                    Title = "Elden Ring",
                    CoverImageUrl = "https://images.igdb.com/igdb/image/upload/t_cover_big/co4jni.jpg",
                    Description = "Action RPG with open world.",
                    ReleaseDate = DateTime.SpecifyKind(new DateTime(2022, 2, 25), DateTimeKind.Utc),
                    Developer = "FromSoftware",
                    Publisher = "Bandai Namco",
                    GenreId = genres.First(g => g.Name == "Action").Id,
                    Rating = 9.7m,

                    MinimumOS = "Windows 10",
                    MinimumProcessor = "Intel Core i5-8400",
                    MinimumRAM = "12 GB",
                    MinimumGraphics = "GTX 1060",
                    MinimumStorage = "60 GB"
                },

                new Game
                {
                    Title = "Civilization VI",
                    CoverImageUrl = "https://images.igdb.com/igdb/image/upload/t_cover_big/co2x4k.jpg",
                    Description = "Turn-based strategy game.",
                    ReleaseDate = DateTime.SpecifyKind(new DateTime(2016, 10, 21), DateTimeKind.Utc),
                    Developer = "Firaxis Games",
                    Publisher = "2K",
                    GenreId = genres.First(g => g.Name == "Strategy").Id,
                    Rating = 9.0m,

                    MinimumOS = "Windows 7",
                    MinimumProcessor = "Intel Core i3",
                    MinimumRAM = "4 GB",
                    MinimumGraphics = "GTX 450",
                    MinimumStorage = "12 GB"
                }
            };

            await context.Games.AddRangeAsync(games);
            await context.SaveChangesAsync();
        }

        if (!await context.Users.AnyAsync())
        {
            var adminUser = new User
            {
                Username = "admin",
                Email = "admin@test.com",
                PasswordHash = Convert.ToBase64String(
                    System.Security.Cryptography.SHA256.HashData(
                        System.Text.Encoding.UTF8.GetBytes("admin123"))),
                Role = "Admin"
            };

            await context.Users.AddAsync(adminUser);
            await context.SaveChangesAsync();
        }
    }
}