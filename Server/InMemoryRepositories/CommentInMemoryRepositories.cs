using Entities;
using RepositoryContracts;

namespace InMemoryRepositories;

public class CommentInMemoryRepositories : ICommentRepository
{
    List<Comment> comments = new();
    List<CommentReaction> commentReactions = new();

    public CommentInMemoryRepositories()
    {
        _ = AddAsync(new Comment("Cats are great!", 1, 1)).Result;
        _ = AddAsync(new Comment("So true!", 1, 2)).Result;
        _ = AddAsync(new Comment("They're just so fluffy", 1, 2)).Result;
        _ = AddAsync(new Comment("Mine's hairless!", 1, 1)).Result;
        _ = AddAsync(new Comment("Is it sick?!", 1, 4)).Result;
        
        _ = AddAsync(new Comment("Cats are still great!", 2, 2)).Result;
        _ = AddAsync(new Comment("Man, mine just spat out a dead mouse :(", 2, 3)).Result;
        _ = AddAsync(new Comment("That's a compliment",2, 2)).Result;
        _ = AddAsync(new Comment("No rats around my house!", 2, 1)).Result;

        _ = AddAsync(new Comment("#FIRST", 3, 1)).Result;
        _ = AddAsync(new Comment("They're just so happy and loving", 3, 2)).Result;
        _ = AddAsync(new Comment("Too noisy for me!", 3, 4)).Result;
        _ = AddAsync(new Comment("Uhhh, no?? Cats forever", 3, 4)).Result;
        
        _ = AddAsync(new Comment("Weather is just the greatest!", 4, 4)).Result;
        _ = AddAsync(new Comment("Not today! It's raining :(", 4, 3)).Result;
        _ = AddAsync(new Comment("Rain just smells so nice", 4, 4)).Result;
        _ = AddAsync(new Comment("Weirdo :O", 4, 1)).Result;
        
        _ = AddAsync(new Comment("HELP!?", 5, 1)).Result;
        _ = AddAsync(new Comment("How do I even do anything?", 5, 1)).Result;
        _ = AddAsync(new Comment("I don't understand async", 5, 2)).Result;
        _ = AddAsync(new Comment("What do you need help with?", 5, 3)).Result;
        
        _ = AddAsync(new Comment("Eh, what? Your carpet?", 6, 2)).Result;
        _ = AddAsync(new Comment("I like my Mowinator3000, it just works", 6, 4)).Result;
        _ = AddAsync(new Comment("It just grows out of control!", 6, 3)).Result;
        _ = AddAsync(new Comment("What color is your carpet?", 6, 1)).Result;
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