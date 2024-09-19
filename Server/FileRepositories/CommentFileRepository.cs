using System.Text.Json;
using Entities;
using RepositoryContracts;

namespace FileRepositories;

public class CommentFileRepository : ICommentRepository
{
    private readonly string filePath = "comments.json";
    
    public CommentFileRepository()
    {
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "[]");
        }
    }
    
    public async Task<List<Comment>> ReadData()
    {
        string commentsAsJson = await File.ReadAllTextAsync(filePath);
        return JsonSerializer.Deserialize<List<Comment>>(commentsAsJson)!;
    }
    
    private async Task WriteData(List<Comment> comments)
    {
        string commentsAsJson = JsonSerializer.Serialize(comments);
        await File.WriteAllTextAsync(filePath, commentsAsJson);
    }
    
    public async Task<Comment> AddAsync(Comment comment)
    {
        List<Comment> comments = await ReadData();
        int maxId = comments.Count > 0 ? comments.Max(u => u.Id) : 0;
        comment.Id = maxId + 1;
        comments.Add(comment);
        await WriteData(comments);
        return comment;
    }

    public async Task UpdateAsync(Comment comment)
    {
        List<Comment> comments = await ReadData();
        Comment? commentToUpdate = comments.FirstOrDefault(u => u.Id == comment.Id);

        if (commentToUpdate == null)
        {
            throw new InvalidOperationException($"Comment with ID '{comment.Id}' not found'");
        }
        
        comments.Remove(commentToUpdate);
        comments.Add(comment);
        await WriteData(comments);
}

    public async Task DeleteAsync(int id)
    {
        List<Comment> comments = await ReadData();
        Comment? commentToDelete = comments.Find(u => u.Id == id);
        
        if (commentToDelete == null)
        {
            throw new InvalidOperationException(
                $"Comment with ID '{id}' not found'");
        }
        
        comments.Remove(commentToDelete);
        await WriteData(comments); 
    }

    public async Task<Comment> GetSingleAsync(int id)
    {
        List<Comment> comments = await ReadData();
        Comment? commentToGet = comments.Find(u => u.Id == id);
        
        if (commentToGet is null)
        {
            throw new InvalidOperationException(
                $"Post with ID '{id}' not found");
        }

        return commentToGet;
    }

    public IQueryable<Comment> GetMany()
    {
        string postsAsJson = File.ReadAllTextAsync(filePath).Result;
        List<Comment> posts = JsonSerializer.Deserialize<List<Comment>>(postsAsJson) !;
        return posts.AsQueryable();
    }

    public Task<CommentReaction> AddReaction(CommentReaction reaction)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(CommentReaction reaction)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsyncReaction(int id)
    {
        throw new NotImplementedException();
    }

    public Task<CommentReaction> GetSingleReaction(int id)
    {
        throw new NotImplementedException();
    }

    public IQueryable<CommentReaction> getManyReactions()
    {
        throw new NotImplementedException();
    }
}