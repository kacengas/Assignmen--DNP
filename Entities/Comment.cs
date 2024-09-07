namespace Entities;

public class Comment
{
    public Comment(int id, string content, DateTime date, User user)
    {
        Id = id;
        Content = content;
        Date = date;
        User = user;
    }

    public int Id { get; set; }

    public string Content { get; set; }

    public DateTime Date { get; set; }

    public User User { get; set; }
}