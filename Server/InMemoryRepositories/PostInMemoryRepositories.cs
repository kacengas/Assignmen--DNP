using System.Formats.Tar;
using Entities;
using RepositoryContracts;

namespace InMemoryRepositories;

public class PostInMemoryRepositories : IPostRepository
{
    List<Post> posts = new();
    List<PostReaction> postReactions = new();

    // public PostInMemoryRepositories()
    // {
    //     _ = AddAsync(new Post(1,"Cat discussion", "Cats are pretty neat, sometimes.")).Result;
    //     _ = AddAsync(new Post(1,"Cat discussion 2", "Cat dropped a dead bird in my bed. No longer neat.")).Result;
    //     _ = AddAsync(new Post(3,"Dog discussion", "Dogs are just far superior to cats. EOD.")).Result;
    //     _ = AddAsync(new Post(2,"Weather?", "So, does anyone else like weather?")).Result;
    //     _ = AddAsync(new Post(4,"DNP QA", "This post is for DNP discussions, or if you need help with stuff.")).Result;
    //     _ = AddAsync(new Post(3,"Best lawn mower?", "What's the bet lawn mower robot to mow my living room carpet?")).Result;
    // }
    
    public Task<Post> AddAsync(Post post)
    {
        post.Id = posts.Any()
            ? posts.Max(p => p.Id) + 1 : 1;
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

    public Task DeleteAsync(int id)
    {
        Post? postToRemove = posts.SingleOrDefault(p => p.Id == id);
        if (postToRemove is null)
        {
            throw new InvalidOperationException(
                $"Post with ID '{id}' not found'");
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
        reaction.Id = posts.Any()
            ? postReactions.Max(pr => pr.Id) + 1
            : 1;
        postReactions.Add(reaction);
        return Task.FromResult(reaction);
    }

    public Task UpdateReactionAsync(PostReaction reaction)
    {
        PostReaction? existingPostReaction = postReactions.SingleOrDefault(pr => pr.Id == reaction.Id);
        if (existingPostReaction is null)
        {
            throw new InvalidOperationException(
                $"Post with ID '{reaction.Id}' not found");
        }
        
        postReactions.Remove(existingPostReaction);
        postReactions.Add(reaction);
        
        return Task.CompletedTask;
    }

    public Task DeleteReactionAsync(int id)
    {
        PostReaction? postReactionToRemove = postReactions.SingleOrDefault(p => p.Id == id);
        if (postReactionToRemove is null)
        {
            throw new InvalidOperationException(
                $"Post with ID '{id}' not found'");
        }
        
        postReactions.Remove(postReactionToRemove);
        return Task.CompletedTask;
    }

    public Task<PostReaction> GetSingleReaction(int id)
    {
        PostReaction? postReactionToGet = postReactions.SingleOrDefault(pr => pr.Id == id);
        if (postReactionToGet is null)
        {
            throw new InvalidOperationException(
                $"Post with ID '{id}' not found");
        }
       
        return Task.FromResult(postReactionToGet);
    }

    public IQueryable<PostReaction> GetManyReactions()
    {
        return postReactions.AsQueryable();
    }
}