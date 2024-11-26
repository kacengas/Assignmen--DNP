namespace Entities;

public class Post
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime Date { get; set; }
    
    private Post() {}

    public Post(string title, string content, int userId)
    {
        Title = title;
        Content = content;
        UserId = userId;
        Date = DateTime.Now;
    }
}