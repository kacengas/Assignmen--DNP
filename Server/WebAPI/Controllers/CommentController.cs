using DTOs;
using Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]

public class CommentController
{
    private readonly ICommentRepository commentRepository;
    private readonly IUserRepository userRepository;
    private readonly IPostRepository postRepository;
    
    public CommentController(ICommentRepository commentRepository, IPostRepository postRepository, IUserRepository userRepository)
    {
        this.commentRepository = commentRepository;
        this.userRepository = userRepository;
        this.postRepository = postRepository;
    }
    
    // POST https://localhost:7276/Comment
    [HttpPost]
    public async Task<IResult> CreateComment([FromBody]CreateCommentDto request)
    {
        Comment comment = new Comment
        {
            Content = request.Content,
            PostId = request.PostId,
            UserId = request.UserId,
            Date = DateTime.Now
        };
        
        await commentRepository.AddAsync(comment);
        return Results.Created($"comments/{comment.Id}", comment);
    }
    
    // GET https://localhost:7276/Comment/{id}
    [HttpGet("{id}")]
    public async Task<IResult> GetSingleComment([FromRoute]int id)
    {
        Comment result = await commentRepository.GetSingleAsync(id);
        return Results.Ok(result);
    }
    
    // DELETE https://localhost:7276/Comment/{id}
    [HttpDelete("{id}")]
    public async Task<IResult> DeleteComment([FromRoute]int id)
    {
        Comment comment = await commentRepository.GetSingleAsync(id);
        if (comment == null)
        {
            return Results.NotFound();
        }
        
        await commentRepository.DeleteAsync(comment.Id);
        return Results.NoContent();
    }
    
    // PUT https://localhost:7276/Comment/{id}
    [HttpPut("{id}")]
    public async Task<IResult> UpdateComment([FromRoute]int id, [FromBody]UpdateCommentDto request)
    {
        Comment comment = await commentRepository.GetSingleAsync(id);
        if (comment == null)
        {
            return Results.NotFound();
        }
        
        comment.Content = request.Content;
        await commentRepository.UpdateAsync(comment);
        return Results.Ok(comment);
    }
    
    // GET https://localhost:7276/Comment
    [HttpGet]
    public async Task<IResult> GetComments([FromQuery] int? userId, [FromQuery] string? userName, [FromQuery] int? postId)
    {
        IQueryable<Comment> comments = commentRepository.GetMany();

        if (userId.HasValue)
        {
            comments = comments.Where(c => c.UserId == userId.Value);
        }

        if (!string.IsNullOrEmpty(userName))
        {
            var user = await userRepository.GetSingleAsync(userId.Value);
            if (user != null)
            {
                comments = comments.Where(c => c.UserId == user.Id);
            }
        }

        if (postId.HasValue)
        {
            comments = comments.Where(c => c.PostId == postId.Value);
        }

        return Results.Ok(comments);
    }
}