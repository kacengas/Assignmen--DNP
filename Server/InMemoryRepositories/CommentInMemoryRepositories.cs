using Entities;
using RepositoryContracts;

namespace InMemoryRepositories;

public class CommentInMemoryRepositories : ICommentRepository
{
    List<Comment> comments = new();
    List<CommentReaction> commentReactions = new();

    public CommentInMemoryRepositories()
    {

    }
    
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

    public Task DeleteAsync(int id)
    {
        Comment? commentToRemove = comments.SingleOrDefault(c => c.Id == id);
        if (commentToRemove is null)
        {
            throw new InvalidOperationException(
                $"Comment with ID '{id}' not found'");
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

    public Task<CommentReaction> AddReaction(CommentReaction reaction)
    {
        reaction.Id = commentReactions.Any()
            ? commentReactions.Max(cr => cr.Id) + 1
            : 1;
        commentReactions.Add(reaction);
        return Task.FromResult(reaction);
    }

    public Task UpdateAsync(CommentReaction reaction)
    {
        CommentReaction? existingReaction = commentReactions.SingleOrDefault(er => er.Id == reaction.Id);
        if (existingReaction is null)
        {
            throw new InvalidOperationException(
                $"Comment with ID '{reaction.Id}' not found");
        }
        
        commentReactions.Remove(existingReaction);
        commentReactions.Add(reaction);
        
        return Task.CompletedTask;
    }

    public Task DeleteAsyncReaction(int id)
    {
        CommentReaction? commentReactionToRemove = commentReactions.SingleOrDefault(cr => cr.Id == id);
        if (commentReactionToRemove is null)
        {
            throw new InvalidOperationException(
                $"Comment with ID '{id}' not found'");
        }
        
        commentReactions.Remove(commentReactionToRemove);
        return Task.CompletedTask;
    }

    public Task<CommentReaction> GetSingleReaction(int id)
    {
        CommentReaction? commentReactionToGet = commentReactions.SingleOrDefault(cr => cr.Id == id);
        if (commentReactionToGet is null)
        {
            throw new InvalidOperationException(
                $"Reaction with ID '{id}' not found");
        }
       
        return Task.FromResult(commentReactionToGet);
    }

    public IQueryable<CommentReaction> getManyReactions()
    {
        return commentReactions.AsQueryable();
    }
}