using Entities;
using RepositoryContracts;

namespace InMemoryRepositories;

public class CommentInMemoryRepositories : ICommentRepository
{
    List<Comment> comments = new();
    
    public Task<Comment> AddAsync(Comment comment)
    {
        comment.Id = comments.Any()
            ? comments.Max(p => p.Id) + 1
            : 1;
        comments.Add(comment);
        return Task.FromResult(comment);
    }

    public Task UpdateAsync(Comment comment)
    {
        Comment? existingComment = comments.SingleOrDefault(c => c.Id == comment.Id);
        if (existingComment is null)
        {
            throw new InvalidOperationException(
                $"Comment with ID '{comment.Id}' not found");
        }
        
        comments.Remove(existingComment);
        comments.Add(comment);
        
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Comment comment)
    {
        Comment? commentToRemove = comments.SingleOrDefault(c => c.Id == comment.Id);
        if (commentToRemove is null)
        {
            throw new InvalidOperationException(
                $"Comment with ID '{comment.Id}' not found'");
        }
        
        comments.Remove(commentToRemove);
        return Task.CompletedTask;
    }

    public Task<Comment> GetSingleAsync(int id)
    {
        Comment? commentToGet = comments.SingleOrDefault(c => c.Id == id);
        if (commentToGet is null)
        {
            throw new InvalidOperationException(
                $"Comment with ID '{id}' not found");
        }
       
        return Task.FromResult(commentToGet);
    }

    public IQueryable<Comment> GetMany()
    {
        return comments.AsQueryable();
    }

    public Task<CommentReaction> AddReaction(PostReaction reaction)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(CommentReaction reaction)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(CommentReaction reaction)
    {
        throw new NotImplementedException();
    }

    public Task<CommentReaction> GetSingleReaction(int id)
    {
        throw new NotImplementedException();
    }

    public IQueryable<PostReaction> getManyReactions()
    {
        throw new NotImplementedException();
    }
}