using Microsoft.AspNetCore.Mvc;
using auth_service.DTOs;
using auth_service.Services;
using System.Net.Http.Json; // Necessary for PostAsJsonAsync

namespace auth_service.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase 
{
    private readonly ITokenService _tokenService;
    private readonly IHttpClientFactory _httpClientFactory;

    // We inject the HttpClientFactory instead of a DbContext
    public AuthController(ITokenService tokenService, IHttpClientFactory httpClientFactory) 
    {
        _tokenService = tokenService;
        _httpClientFactory = httpClientFactory;
    }

    // [HttpPost("login")]
    // public async Task<IActionResult> Login([FromBody] LoginRequest request) 
    // {
    //     var client = _httpClientFactory.CreateClient();

    
    //     var response = await client.PostAsJsonAsync("http://localhost:5001/api/user/verify", request);

    //     if (response.IsSuccessStatusCode) 
    //     {
    //         var token = _tokenService.CreateToken(request.Email);
    //         return Ok(new AuthResponse { Token = token, Email = request.Email });
    //     }

    //     return Unauthorized("Invalid email or password.");
    // }

    [HttpPost("login")]
public IActionResult Login([FromBody] LoginRequest request)
{
    // HARDCODED BYPASS FOR DEMO
    if (request.Email == "admin@goride.com" && request.Password == "admin123")
    {
        return Ok(new { 
            token = "mock-jwt-token-abcd-1234", 
            role = "Admin",
            fullName = "System Admin"
        });
    }

    // Original database logic (commented out or as fallback)
    return Unauthorized(new { message = "Invalid credentials" });
}
}