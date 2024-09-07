namespace Entities;

public class CommentReaction
{
    public CommentReaction(int commentId, int userId, Enum type)
    {
        CommentId = commentId;
        UserId = userId;
        Type = type;
    }
    
    public int UserId { get; set; }
    public int CommentId { get; set; }
    public Enum Type { get; set; }
}