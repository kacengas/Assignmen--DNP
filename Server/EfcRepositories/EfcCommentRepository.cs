using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RepositoryContracts;

namespace EfcRepositories;

public class EfcCommentRepository : ICommentRepository
{
    private readonly AppContext _context;

    public EfcCommentRepository(AppContext context)
    {
        _context = context;
    }

    public async Task<Comment> FindCommentById(int id)
    {
        return await _context.Comments.FindAsync(id);
    }

    public async Task<Comment> AddAsync(Comment comment)
    {
        EntityEntry<Comment> entityEntry = await _context.Comments.AddAsync(comment);
        await _context.SaveChangesAsync();
        return entityEntry.Entity;
    }

    public async Task UpdateAsync(Comment comment)
    {
        if (!await _context.Comments.AnyAsync(c => c.Id == comment.Id))
        {
            throw new InvalidOperationException("Comment not found");
        }

        _context.Comments.Update(comment);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var comment = await _context.Comments.FindAsync(id);
        if (comment == null)
        {
            throw new InvalidOperationException("Comment not found");
        }

        _context.Comments.Remove(comment);
        await _context.SaveChangesAsync();
    }

    public async Task<Comment> GetSingleAsync(int id)
    {
        return await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);
    }

    public IQueryable<Comment> GetMany()
    {
        return _context.Comments.AsQueryable();
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