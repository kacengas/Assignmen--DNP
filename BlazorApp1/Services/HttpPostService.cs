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
        HttpResponseMessage httpResponse = await client.PostAsJsonAsync("api/post", createPostDto);
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
    
    public async Task<PostDto> GetPostAsync(int id)
    {
        HttpResponseMessage httpResponse = await client.GetAsync($"api/post/{id}");
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception($"Failed to retrieve post. Status Code: {httpResponse.StatusCode}, Response: {response}");
        }
        return JsonSerializer.Deserialize<PostDto>(response, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
    }

}