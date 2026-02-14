using admin_service.Dtos;
using System.Text.Json;

namespace admin_service.Services;

public class UserServiceClient : IUserServiceClient
{
    private readonly HttpClient _httpClient;

    public UserServiceClient(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("UserService");
    }

    public async Task<List<UserDto>> GetUsersAsync()
    {
        //this code makes communicate admin service to user service
        try {
            //admin service sends request to user service
            var response = await _httpClient.GetAsync("user");

            //if user service is down this avoids crashing admin service
            if (!response.IsSuccessStatusCode) return new List<UserDto>();
            
            //converts stream of bytes into json string (text)
            var content = await response.Content.ReadAsStringAsync();

            //JsonSerializer - converts json object to c# object
            return JsonSerializer.Deserialize<List<UserDto>>(content, 
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<UserDto>();
        } catch {
            return new List<UserDto>();
        }
    }
}