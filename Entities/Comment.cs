namespace Entities;

public class Comment
{
    public Comment(string content, DateTime date, int userId, int postId)
    {
        Content = content;
        Date = date;
        UserId = userId;
        PostId = postId;
    }

    public int Id { get; set; }

    public string Content { get; set; }

    public DateTime Date { get; set; }

    public int UserId { get;}
    
    public int PostId { get;}
}