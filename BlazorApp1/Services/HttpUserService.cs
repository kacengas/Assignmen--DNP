using System.Text.Json;
using DTOs;

namespace BlazorApp1.Services;

public class HttpUserService : IUserService
{
    private readonly HttpClient client;
    
    public HttpUserService(HttpClient client)
    {
        this.client = client;
    }
    
    public async Task<CreateUserDTO> AddUserAsync(CreateUserDTO createUserDto)
    {
        HttpResponseMessage httpResponse = await client.PostAsJsonAsync("user", createUserDto);
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(response);
        }
        return JsonSerializer.Deserialize<CreateUserDTO>(response, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
    }
    
    public async Task<UserDTO> GetUserAsync(int id)
    {
        HttpResponseMessage httpResponse = await client.GetAsync($"user/{id}");
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(response);
        }
        return JsonSerializer.Deserialize<UserDTO>(response, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
    }

    public Task GetByUsernameAsync(string username)
    {
        throw new NotImplementedException();
    }
    
}
