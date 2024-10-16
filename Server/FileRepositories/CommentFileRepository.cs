using System.Net;
using System.Text.Json;
using Entities;
using RepositoryContracts;

namespace FileRepositories;

public class CommentFileRepository : ICommentRepository
{
    private readonly string filePath = "comments.json";
    private readonly string reactionsFilePath = "commentReactions.json";
    
    public CommentFileRepository()
    {
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "[]");
        }

        if (!File.Exists(reactionsFilePath))
        {
            File.WriteAllText(reactionsFilePath, "[]");
        }
    }
    
    public async Task<List<Comment>> ReadData()
    {
        string commentsAsJson = await File.ReadAllTextAsync(filePath);
        return JsonSerializer.Deserialize<List<Comment>>(commentsAsJson)!;
    }
    
    private async Task WriteData(List<Comment> comments)
    {
        string commentsAsJson = JsonSerializer.Serialize(comments, new JsonSerializerOptions { WriteIndented = true });
        await File.WriteAllTextAsync(filePath, commentsAsJson);
    }
    
    public async Task<List<CommentReaction>> ReadReactionData()
    {
        string commentsAsJson = await File.ReadAllTextAsync(reactionsFilePath);
        return JsonSerializer.Deserialize<List<CommentReaction>>(commentsAsJson)!;
    }
    
    public async Task WriteReactionData(List<CommentReaction> reactions)
    {
        string commentsAsJson = JsonSerializer.Serialize(reactions, new JsonSerializerOptions { WriteIndented = true });
        await File.WriteAllTextAsync(reactionsFilePath, commentsAsJson);
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
                $"Comment with ID '{id}' not found");
        }

        return commentToGet;
    }

    public IQueryable<Comment> GetMany()
    {
        string commentsAsJson = File.ReadAllTextAsync(filePath).Result;
        List<Comment> comments = JsonSerializer.Deserialize<List<Comment>>(commentsAsJson)!;
        return comments.AsQueryable();
    }

    public async Task<CommentReaction> AddReaction(CommentReaction reaction)
    {
        List<CommentReaction> reactions = await ReadReactionData();
        int maxId = reactions.Count > 0 ? reactions.Max(r => r.Id) : 0;
        reaction.Id = maxId + 1;
        reactions.Add(reaction);
        await WriteReactionData(reactions);
        return reaction;
    }

    public async Task UpdateAsync(CommentReaction reaction)
    {
        List<CommentReaction> reactions = await ReadReactionData();
        CommentReaction? reactionToUpdate = reactions.Find(r => r.Id == reaction.Id);
    
        if (reactionToUpdate == null)
        {
            throw new InvalidOperationException($"Reaction with ID '{reaction.Id}' not found'");
        }
        
        reactions.Remove(reactionToUpdate);
        reactions.Add(reaction);
        await WriteReactionData(reactions);
    }

    public async Task DeleteAsyncReaction(int id)
    {
        List<CommentReaction> reactions = await ReadReactionData();
        CommentReaction? reactionToDelete = reactions.Find(r => r.Id == id);

        if (reactionToDelete == null)
        {
            throw new InvalidOperationException($"Reaction with ID '{id}' not found'");
        }
        
        reactions.Remove(reactionToDelete);
        await WriteReactionData(reactions);
    }

    public async Task<CommentReaction> GetSingleReaction(int id)
    {
        List<CommentReaction> reactions = await ReadReactionData();
        CommentReaction? reactionToGet = reactions.Find(r => r.Id == id);
        
        if (reactionToGet is null)
        {
            throw new InvalidOperationException($"Reaction with ID '{id}' not found");
        }
        
        return reactionToGet;
    }

    public IQueryable<CommentReaction> getManyReactions()
    {
        List<CommentReaction> reactions = ReadReactionData().Result;
        return reactions.AsQueryable();
    }
}