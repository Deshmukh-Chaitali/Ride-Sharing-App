using admin_service.Dtos;

namespace admin_service.Services;

public interface IUserServiceClient
{
    Task<List<UserDto>> GetUsersAsync();
}