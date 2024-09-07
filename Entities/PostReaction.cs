namespace Entities;

public class PostReaction
{
    public PostReaction(int postId, int userId, Enum type)
    {
        PostId = postId;
        UserId = userId;
        Type = type;
    }
    
    public int PostId { get; set; }
    public int UserId { get; set; }
    public Enum Type { get; set; }
}