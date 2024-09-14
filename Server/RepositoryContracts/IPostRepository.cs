using Entities;

namespace RepositoryContracts;

public interface IPostRepository
{
    Task<Post> AddAsync(Post post);
    Task UpdateAsync(Post post);
    Task DeleteAsync(int id);
    Task<Post> GetSingleAsync(int id);
    IQueryable<Post> GetMany();
    Task<PostReaction> AddReaction(PostReaction reaction);
    Task UpdateAsync(PostReaction reaction);
    Task DeleteAsyncReaction(int id);
    Task<PostReaction> GetSingleReaction(int id);
    IQueryable<PostReaction> GetManyReactions(); 
}