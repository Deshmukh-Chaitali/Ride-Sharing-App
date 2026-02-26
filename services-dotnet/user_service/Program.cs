using Microsoft.EntityFrameworkCore;
using user_service.Data;

var builder = WebApplication.CreateBuilder(args);

// 1. Safe Connection String retrieval
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

// 2. Register DbContext with the Official MySQL Driver
builder.Services.AddDbContext<UserDbContext>(options =>
    options.UseMySQL(connectionString));

builder.Services.AddControllers();

// 3. CORS Configuration
builder.Services.AddCors(options => {
    options.AddPolicy("AllowAll", policy => {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// 4. Middleware Order
app.UseRouting(); // Good practice to include this explicitly
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();

app.Run();