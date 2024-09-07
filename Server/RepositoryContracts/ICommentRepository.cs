using Entities;

namespace RepositoryContracts;

public interface ICommentRepository
{
    Task<Comment> AddAsync(Comment comment);
    Task UpdateAsync(Comment comment);
    Task DeleteAsync(Comment comment);
    Task<Comment> GetSingleAsync(int id);
    IQueryable<Comment> GetMany();
    Task<CommentReaction> AddReaction(PostReaction reaction);
    Task UpdateAsync(CommentReaction reaction);
    Task DeleteAsync(CommentReaction reaction);
    Task<CommentReaction> GetSingleReaction(int commentId, int userId);
    //IQueryable<PostReaction> GetMany(); ??? 
}