using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using user_service.Data;
using user_service.Models;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly UserDbContext _context;

    public UserController(UserDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    {
        return await _context.Users.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUser(Guid id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return NotFound();
        return user;
    }

    [HttpPost("register")]
    public async Task<ActionResult<User>> Register(UserRegistrationDto userRegistrationDto)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            FullName = userRegistrationDto.FullName,
            Email = userRegistrationDto.Email,
            PhoneNumber = userRegistrationDto.PhoneNumber,
            UserRole = userRegistrationDto.UserRole,
            Rating = userRegistrationDto.Rating 
        };

        _context.Users.Add(user); // Fixed to uppercase
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
    }

    [HttpPatch("{id}/rating")]
    public async Task<IActionResult> UpdateRating(Guid id, [FromBody] double newRating)
    {
        var user = await _context.Users.FindAsync(id); // Fixed to uppercase
        if (user == null) return NotFound();

        user.Rating = (user.Rating + newRating) / 2;

        await _context.SaveChangesAsync();
        return NoContent();
    }
}