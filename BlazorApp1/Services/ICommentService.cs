using DTOs;

namespace BlazorApp1.Services;

public interface ICommentService
{
    Task<CreateCommentDTO> AddCommentAsync(CreateCommentDTO createCommentDto);
    Task<List<CreateCommentDTO>> GetCommentsAsync(int postId);
}