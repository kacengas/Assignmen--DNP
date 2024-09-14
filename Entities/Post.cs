namespace Entities;

public class Post
{
    public Post(int userId,string title, string content)
    {
        UserId = userId;
        Title = title;
        Content = content;
        Date = DateTime.Now;
    }
    
    public int Id { get; set; }
    public int UserId { get; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime Date { get; set; }
}