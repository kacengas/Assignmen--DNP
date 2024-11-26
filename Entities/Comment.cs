namespace Entities;

public class Comment
{
    public int Id { get; set; }

    public string Content { get; set; }

    public DateTime Date { get; set; }

    public int UserId { get; set; }
    
    public int PostId { get; set; }
    
    private Comment() {}
    
    public Comment(string content, int userId, int postId)
    {
        Content = content;
        UserId = userId;
        PostId = postId;
        Date = DateTime.Now;
    }
}