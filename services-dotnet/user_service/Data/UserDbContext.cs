using Microsoft.EntityFrameworkCore;
using user_service.Models;

namespace user_service.Data;

public class UserDbContext : DbContext
{
    // This constructor allows Program.cs to "inject" the connection
    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }

    // REMOVE the OnConfiguring method entirely. 
    // It is better to manage connections in one place (Program.cs).
}