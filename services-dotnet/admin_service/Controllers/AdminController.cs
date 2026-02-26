using Microsoft.AspNetCore.Mvc;
using admin_service.Services; 
using admin_service.Dtos;    

namespace admin_service.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AdminController : ControllerBase
{
    private readonly IUserServiceClient _userClient;
    private readonly IPricingService _pricingService;

    public AdminController(IUserServiceClient userClient, IPricingService pricingService)
    {
        _userClient = userClient;
        _pricingService = pricingService;
    }

    //shows count of driver , riders and admin
    [HttpGet("stats")] 
    public async Task<IActionResult> GetStats()
    {
        var users = await _userClient.GetUsersAsync();

        if (users == null || !users.Any())
            return Ok(new { Message = "No users found" });

        var stats = new
        {
            TotalUsers = users.Count,
            Admins = users.Count(u => u.UserRole == "Admin"),
            Drivers = users.Count(u => u.UserRole == "Driver"),
            Riders = users.Count(u => u.UserRole == "Rider")
        };

        return Ok(stats);
    }

    // GET current pricing
    [HttpGet("pricing")]
    public IActionResult GetPricing()
    {
        return Ok(_pricingService.GetCurrentPricing());
    }

    // POST to update pricing 
    [HttpPost("pricing/update")]
    public IActionResult UpdatePricing([FromBody] PricingDto newPricing)
    {
        _pricingService.UpdatePricing(newPricing);
        return Ok(new { Message = "Pricing updated successfully", Current = _pricingService.GetCurrentPricing() });
    }

    [HttpGet("estimate")]
public IActionResult GetEstimate([FromQuery] double distance)
{
    var fare = _pricingService.CalculateEstimatedFare(distance);
    return Ok(new { Distance = distance, EstimatedFare = fare });
}

    // Rainy season doubles pricing
    // [HttpPost("surge-toggle")]
    // public IActionResult ToggleSurge([FromQuery] bool isRainy)
    // {
    //     _pricingService.SetSurgeMode(isRainy);
    //     var status = isRainy ? "Activated" : "Deactivated";
    
    //     return Ok(new { 
    //     Message = $"Surge Pricing {status}", 
    //     CurrentPricing = _pricingService.GetCurrentPricing() 
    // });

    //searches user by using phone number
    // [HttpGet("search/{phone}")]
    // async Task<IActionResult> SearchByPhone(string phone)
    // {
    //     var users = await _userClient.GetUsersAsync();
    //     var user = users.FirstOrDefault(u => u.PhoneNumber == phone);
    //     if (user == null) return NotFound(new { Message = "User not found" });
    //     return Ok(user);
    // }
}


//1. http://localhost:5241/api/admin/stats
//2. http://localhost:5241/api/admin/pricing
//3. http://localhost:5241/api/admin/pricing/update
//4. http://localhost:5241/api/admin/surge-toggle?isRainy=true
//5. http://localhost:5241/api/admin/surge-toggle?isRainy=false
//6. http://localhost:5241/api/admin/search/8888877777