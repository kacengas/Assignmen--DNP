using Entities;

namespace RepositoryContracts;

public interface IPostRepository
{
    Task<Post> AddAsync(Post post);
    Task UpdateAsync(Post post);
    Task DeleteAsync(Post post);
    Task<Post> GetSingleAsync(int id);
    IQueryable<Post> GetMany();
    Task<PostReaction> AddReaction(PostReaction reaction);
    Task UpdateAsync(PostReaction reaction);
    Task DeleteAsync(PostReaction reaction);
    Task<PostReaction> GetSingleReaction(int postId, int userId);
    //IQueryable<PostReaction> GetMany(); ??? 
}