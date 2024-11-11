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
    
    public async Task<CreatePostDTO> AddPostAsync(CreatePostDTO createPostDto)
    {
        HttpResponseMessage httpResponse = await client.PostAsJsonAsync("post", createPostDto);
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(response);
        }
        return JsonSerializer.Deserialize<CreatePostDTO>(response, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
    }
    
    public async Task<PostDTO> GetAsync(int id)
    {
        HttpResponseMessage httpResponse = await client.GetAsync($"post/{id}");
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception($"Failed to retrieve post. Status Code: {httpResponse.StatusCode}, Response: {response}");
        }
        return JsonSerializer.Deserialize<PostDTO>(response, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
    }
    
    public async Task<IEnumerable<PostDTO>> GetPostsAsync()
    {
        try
        {
            return await client.GetFromJsonAsync<IEnumerable<PostDTO>>("post");
        }
        catch (HttpRequestException ex)
        {
            throw new Exception("Error fetching posts: " + ex.Message, ex);
        }
    }
}