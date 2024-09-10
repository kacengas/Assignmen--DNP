using System.Formats.Tar;
using Entities;
using RepositoryContracts;

namespace InMemoryRepositories;

public class PostInMemoryRepositories : IPostRepository
{
    List<Post> posts = new();
    List<PostReaction> reactions = new();
    
    public Task<Post> AddAsync(Post post)
    {
        post.Id = posts.Any()
            ? posts.Max(p => p.Id) + 1
            : 1;
        posts.Add(post);
        return Task.FromResult(post);
    }

    public Task UpdateAsync(Post post)
    {
        Post? existingPost = posts.SingleOrDefault(p => p.Id == post.Id);
        if (existingPost is null)
        {
            throw new InvalidOperationException(
                $"Post with ID '{post.Id}' not found");
        }
        
        posts.Remove(existingPost);
        posts.Add(post);
        
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Post post)
    {
        Post? postToRemove = posts.SingleOrDefault(p => p.Id == post.Id);
        if (postToRemove is null)
        {
            throw new InvalidOperationException(
                $"Post with ID '{post.Id}' not found'");
        }
        
        posts.Remove(postToRemove);
        return Task.CompletedTask;
    }

    public Task<Post> GetSingleAsync(int id)
    {
       Post? postToGet = posts.SingleOrDefault(p => p.Id == id);
       if (postToGet is null)
       {
           throw new InvalidOperationException(
               $"Post with ID '{id}' not found");
       }
       
       return Task.FromResult(postToGet);
    }

    public IQueryable<Post> GetMany()
    {
        return posts.AsQueryable();
    }

    public Task<PostReaction> AddReaction(PostReaction reaction)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(PostReaction reaction)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(PostReaction reaction)
    {
        throw new NotImplementedException();
    }

    public Task<PostReaction> GetSingleReaction(int id)
    {
        throw new NotImplementedException();
    }

    public IQueryable<PostReaction> GetManyReactions()
    {
        return reactions.AsQueryable();
    }
}