using System.Text.Json;
using DTOs;

namespace BlazorApp1.Services;

public class HttpCommentService : ICommentService
{
    private readonly HttpClient client;
    
    public HttpCommentService(HttpClient client)
    {
        this.client = client;
    }
    
    public async Task<CreateCommentDto> AddCommentAsync(CreateCommentDto createCommentDto)
    {
        HttpResponseMessage httpResponse = await client.PostAsJsonAsync($"comment", createCommentDto);
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception($"Failed to retrieve post. Status Code: {httpResponse.StatusCode}, Response: {response}");
        }
        return JsonSerializer.Deserialize<CreateCommentDto>(response, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
    }

    public async Task<List<CreateCommentDto>> GetCommentsAsync(int postId)
    {
        HttpResponseMessage response = await client.GetAsync($"comment");
        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return new List<CreateCommentDto>();
        }
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<List<CreateCommentDto>>();
    }
}