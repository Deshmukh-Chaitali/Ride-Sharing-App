using Microsoft.EntityFrameworkCore;
using payment_service.Data;
using payment_service.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// 1. Configure MySQL Database Connection
// Ensure your appsettings.json has a "DefaultConnection" string
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<PaymentDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// 2. Register Dependency Injection (DI)
builder.Services.AddScoped<IPaymentGateway, PaymentGateway>();

// 3. Set the Payment Service Port to 5005
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenLocalhost(5005);
});

var app = builder.Build();

// Ensure the database is created (useful for CDAC project demo)
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<PaymentDbContext>();
    db.Database.EnsureCreated();
}

app.UseAuthorization();
app.MapControllers();

app.Run();