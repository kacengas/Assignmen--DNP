using DTOs;

namespace BlazorApp1.Services;

public interface IPostService
{
    Task<CreatePostDTO> AddPostAsync(CreatePostDTO createPostDto);
    Task<PostDTO> GetAsync(int id);
    Task<IEnumerable<PostDTO>> GetPostsAsync();
}