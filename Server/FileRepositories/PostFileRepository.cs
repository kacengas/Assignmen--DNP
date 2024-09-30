using System.Text.Json;
using Entities;
using RepositoryContracts;

namespace FileRepositories;

public class PostFileRepository : IPostRepository
{
    private readonly string filePath = "posts.json";
    private readonly string reactionFilePath = "reactions.json";

    public PostFileRepository()
    {
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "[]");
        }
        if (!File.Exists(reactionFilePath))
        {
            File.WriteAllText(reactionFilePath, "[]");
        }
    }
    
    public async Task<List<Post>> ReadData()
    {
        string postsAsJson = await File.ReadAllTextAsync(filePath);
        return JsonSerializer.Deserialize<List<Post>>(postsAsJson)!;
    }
    
    private async Task WriteData(List<Post> posts)
    {
        string postsAsJson = JsonSerializer.Serialize(posts);
        await File.WriteAllTextAsync(filePath, postsAsJson);
    }
    
    public async Task<List<PostReaction>> ReadDate()
    {
        string commentsAsJson = await File.ReadAllTextAsync(filePath);
        return JsonSerializer.Deserialize<List<PostReaction>>(commentsAsJson)!;
    }
    
    public async Task WriteData(List<PostReaction> reactions)
    {
        string commentsAsJson = JsonSerializer.Serialize(reactions);
        await File.WriteAllTextAsync(filePath, commentsAsJson);
    }
    
    public async Task<Post> AddAsync(Post post)
    {
        List<Post> posts = await ReadData();
        int maxId = posts.Count > 0 ? posts.Max(u => u.Id) : 0;
        post.Id = maxId + 1;
        posts.Add(post);
        await WriteData(posts);
        return post;
    }

    public async Task UpdateAsync(Post post)
    {
        List<Post> posts = await ReadData();
        Post? postToUpdate = posts.Find(u => u.Id == post.Id);
        
        if (postToUpdate == null)
        {
            throw new InvalidOperationException($"Post with ID '{post.Id}' not found'");
        }
        
        posts.Remove(postToUpdate);
        posts.Add(post);
        await WriteData(posts);
    }

    public async Task DeleteAsync(int id)
    {
        List<Post> posts = await ReadData();
        Post? postToDelete = posts.Find(u => u.Id == id);
        
        if (postToDelete == null)
        {
            throw new InvalidOperationException(
                $"Post with ID '{id}' not found'");
        }
        
        posts.Remove(postToDelete);
        await WriteData(posts); 
    }

    public async Task<Post> GetSingleAsync(int id)
    {
        List<Post> posts = await ReadData();
        Post? postToGet = posts.Find(u => u.Id == id);
        
        if (postToGet is null)
        {
            throw new InvalidOperationException(
                $"Post with ID '{id}' not found");
        }

        return postToGet;
    }

    public IQueryable<Post> GetMany()
    {
        string postsAsJson = File.ReadAllTextAsync(filePath).Result;
        List<Post> posts = JsonSerializer.Deserialize<List<Post>>(postsAsJson) !;
        return posts.AsQueryable();
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