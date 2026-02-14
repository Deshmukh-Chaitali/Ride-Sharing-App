using admin_service.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

//registering custom services 
builder.Services.AddScoped<IUserServiceClient, UserServiceClient>();
builder.Services.AddSingleton<IPricingService, PricingService>();

// Admin service references to user service 
builder.Services.AddHttpClient("UserService", client =>
{
    // User Service port to 5228
    client.BaseAddress = new Uri("http://localhost:5228/api/");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

// Admin Service Port to 5241
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenLocalhost(5241);
});

var app = builder.Build();


app.UseAuthorization();
app.MapControllers();

app.Run();