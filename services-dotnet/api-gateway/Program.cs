using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add the ocelot.json file
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

builder.Services.AddOcelot();

// Enable CORS so React can talk to the Gateway
builder.Services.AddCors(options => {
    options.AddPolicy("GatewayPolicy", p => p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

var app = builder.Build();

app.UseCors("GatewayPolicy");

// Start Ocelot
await app.UseOcelot();

app.Run();