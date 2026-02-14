using Microsoft.EntityFrameworkCore;
using user_service.Models;

namespace user_service.Data;

public class UserDbContext : DbContext
{
    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
    {
    }

    // Changed to Uppercase 'Users' to match Controller calls
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // This is fine for local, but Program.cs usually handles this
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseMySQL("server=localhost;port=3306;database=UserDb;user=root;password=root;");
        }
    }
}