using DTOs;

namespace BlazorApp1.Services;

public interface IPostService
{
    Task<CreatePostDto> AddPostAsync(CreatePostDto createPostDto);
}