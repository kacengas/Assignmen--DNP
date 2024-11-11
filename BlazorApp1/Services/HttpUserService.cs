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
    
    public async Task<CreateUserDto> AddUserAsync(CreateUserDto createUserDto)
    {
        HttpResponseMessage httpResponse = await client.PostAsJsonAsync("user", createUserDto);
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(response);
        }
        return JsonSerializer.Deserialize<CreateUserDto>(response, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
    }
    
    public async Task<UserDto> GetUserAsync(int id)
    {
        HttpResponseMessage httpResponse = await client.GetAsync($"user/{id}");
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(response);
        }
        return JsonSerializer.Deserialize<UserDto>(response, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
    }
}
