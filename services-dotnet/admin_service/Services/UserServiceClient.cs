using admin_service.Dtos;
using System.Text.Json;
using System.Net.Http.Json;

namespace admin_service.Services;

public class UserServiceClient : IUserServiceClient
{
    private readonly HttpClient _httpClient;

    public UserServiceClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<UserDto>> GetUsersAsync()
    {
        try {
            var response = await _httpClient.GetAsync("api/user");

            if (!response.IsSuccessStatusCode) return new List<UserDto>();
            
            var content = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<UserDto>>(content, 
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<UserDto>();
        } catch {
            return new List<UserDto>();
        }
    }
}