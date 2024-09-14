using Entities;

namespace RepositoryContracts;

public interface ICommentRepository
{
    Task<Comment> AddAsync(Comment comment);
    Task UpdateAsync(Comment comment);
    Task DeleteAsync(int id);
    Task<Comment> GetSingleAsync(int id);
    IQueryable<Comment> GetMany();
    Task<CommentReaction> AddReaction(CommentReaction reaction);
    Task UpdateAsync(CommentReaction reaction);
    Task DeleteAsyncReaction(int id);
    Task<CommentReaction> GetSingleReaction(int id);
    IQueryable<CommentReaction> getManyReactions(); 
}