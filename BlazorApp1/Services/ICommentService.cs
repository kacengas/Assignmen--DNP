using DTOs;

namespace BlazorApp1.Services;

public interface ICommentService
{
    Task<CreateCommentDto> AddCommentAsync(CreateCommentDto createCommentDto);
    Task<List<CreateCommentDto>> GetCommentsAsync(int postId);
}