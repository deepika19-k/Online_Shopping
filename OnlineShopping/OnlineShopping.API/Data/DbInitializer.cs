using OnlineShopping.API.Models;
using BCrypt.Net;

namespace OnlineShopping.API.Data
{
    public static class DbInitializer
    {
        public static void Seed(ApplicationDbContext context, IConfiguration config)
        {
            context.Database.EnsureCreated();

            if (!context.Users.Any(u => u.Role == "Admin"))
            {
                context.Users.Add(new User
                {
                    FirstName = "Admin",
                    LastName = "User",
                    Email = config["AdminSettings:Email"],
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(config["AdminSettings:Password"]),
                    Role = "Admin"
                });
            }

            if (!context.Categories.Any())
            {
                context.Categories.AddRange(
                    new Category { Name = "Household" },
                    new Category { Name = "Electronics" },
                    new Category { Name = "Stationery" },
                    new Category { Name = "Dresses" }
                );
            }

            context.SaveChanges();
        }
    }
}