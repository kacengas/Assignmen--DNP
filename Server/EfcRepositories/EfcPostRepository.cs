using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RepositoryContracts;

namespace EfcRepositories;

public class EfcPostRepository : IPostRepository
{
    private readonly AppContext ctx;
    
    public EfcPostRepository(AppContext ctx)
    {
        this.ctx = ctx;
    }
    
    public async Task<Post> AddAsync(Post post)
    {
        EntityEntry<Post> result = await ctx.Posts.AddAsync(post);
        await ctx.SaveChangesAsync();
        return result.Entity;
    }

    public async Task UpdateAsync(Post post)
    {
        if (!(await ctx.Posts.AnyAsync(p => p.Id == post.Id)))
        {
            throw new InvalidOperationException($"Post with id {post.Id} not found");
        }
    }

    public async Task DeleteAsync(int id)
    {
        Post? existing = await ctx.Posts.SingleOrDefaultAsync(p => p.Id == id);
        if (existing == null)
        {
            throw new InvalidOperationException($"Post with id {id} not found");
        }
        
        ctx.Posts.Remove(existing);
        await ctx.SaveChangesAsync();
    }

    public async Task<Post> GetSingleAsync(int id)
    {
        return await ctx.Posts.SingleOrDefaultAsync(p => p.Id == id);
    }

    public IQueryable<Post> GetMany()
    {
        return ctx.Posts.AsQueryable();
    }

    public Task<PostReaction> AddReaction(PostReaction reaction)
    {
        throw new NotImplementedException();
    }

    public Task UpdateReactionAsync(PostReaction reaction)
    {
        throw new NotImplementedException();
    }

    public Task DeleteReactionAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<PostReaction> GetSingleReaction(int id)
    {
        throw new NotImplementedException();
    }

    public IQueryable<PostReaction> GetManyReactions()
    {
        throw new NotImplementedException();
    }
}