using System.ComponentModel.DataAnnotations;

public class UserRegistrationDto 
{
    [Required(ErrorMessage = "Full Name is required")]
    [StringLength(100, MinimumLength = 3)]
    public string FullName { get; set; } = string.Empty;

    [Required]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string Email { get; set; } = string.Empty;

    [Required]
    [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be exactly 10 digits")]
    public string PhoneNumber { get; set; } = string.Empty;

    // Added this because the Controller requires it
    [Required]
    public string UserRole { get; set; } = "Rider"; 

    [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5")]
    public double Rating { get; set; }
}