namespace DTOs;

public class UpdateCommentDto
{
    public string Content { get; set; }
    public int UserId { get; set; }
    public int PostId { get; set; }
}