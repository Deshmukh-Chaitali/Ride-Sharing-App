using admin_service.Services; // Tells .NET where to find IPricingService
using Microsoft.OpenApi.Models; // For Swagger if needed

var builder = WebApplication.CreateBuilder(args);

// 1. ADD CORS POLICY
builder.Services.AddCors(options => {
    options.AddPolicy("AllowAll", policy => {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

builder.Services.AddControllers();

// Register your custom services
// If these are in the Services folder, the 'using' above fixes this
builder.Services.AddSingleton<IPricingService, PricingService>(); 
builder.Services.AddHttpClient<IUserServiceClient, UserServiceClient>();

// 2. SWAGGER FIX
// If you don't have Swagger installed, you can comment these two lines out
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

var app = builder.Build();

// 3. ENABLE CORS (Must be before MapControllers)
app.UseCors("AllowAll");

app.UseAuthorization();
app.MapControllers();

app.Run();