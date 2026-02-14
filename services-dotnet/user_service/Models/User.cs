
using System.ComponentModel.DataAnnotations;
namespace user_service.Models;
public class User {
    [Key]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Full Name is mandatory")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters")]
    [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Name can only contain letters and spaces")]
    public string FullName { get; set; } = string.Empty;

    [Required]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string Email { get; set; } = string.Empty;

    [Required]
    [Phone(ErrorMessage = "Invalid Phone Number format")]
    [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be exactly 10 digits")]
    public string PhoneNumber { get; set; } = string.Empty;

    [Range(1.0, 5.0, ErrorMessage = "Rating must be between 1.0 and 5.0")]
    public double Rating { get; set; } = 5.0; // Important for ride-sharing!
   
//    [Required]
// [StringLength(100, MinimumLength = 6)]
// public string Password { get; set; } = "admin123";

    [Required]
    [RegularExpression("^(Rider|Driver|Admin)$", ErrorMessage = "Role must be Rider, Driver, or Admin")]
    public string UserRole { get; set; } = "Rider"; // Rider or Driver
}