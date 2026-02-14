var builder = WebApplication.CreateBuilder(args);

// 1. Define the Policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000") // React's URL
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

builder.Services.AddControllers();
// ... other services

var app = builder.Build();

// 2. Use the Policy (Must be BEFORE MapControllers)
app.UseCors("AllowReactApp");

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();