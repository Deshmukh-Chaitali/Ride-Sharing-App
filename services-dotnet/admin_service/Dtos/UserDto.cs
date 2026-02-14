namespace admin_service.Dtos;
public class UserDto
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string UserRole { get; set; } = string.Empty;
    public double Rating { get; set; }
}