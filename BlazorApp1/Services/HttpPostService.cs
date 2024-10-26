using System.Text.Json;
using DTOs;

namespace BlazorApp1.Services;

public class HttpPostService : IPostService
{
    private readonly HttpClient client;
    
    public HttpPostService(HttpClient client)
    {
        this.client = client;
    }
    
    public async Task<CreatePostDto> AddPostAsync(CreatePostDto createPostDto)
    {
        HttpResponseMessage httpResponse = await client.PostAsJsonAsync("api/posts", createPostDto);
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(response);
        }
        return JsonSerializer.Deserialize<CreatePostDto>(response, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
    }
}