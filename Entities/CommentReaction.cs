namespace Entities;

public class CommentReaction
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int CommentId { get; set; }
    public Enum Type { get; set; }
}