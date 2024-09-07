namespace Entities;

public class Post
{
    public Post(int id,User user, string content, DateTime date)
    {
        Id = id;
        User = user;
        Content = content;
        Date = date;
    }
    
    public int Id { get; set; }
    public User User { get; set; }
    public string Content { get; set; }
    public DateTime Date { get; set; }
}