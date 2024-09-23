using RepositoryContracts;

namespace CLI.UI.ManageComments;

public class CommentListView
{
    private readonly ICommentRepository commentRepository;

    public CommentListView(ICommentRepository commentRepository)
    {
        this.commentRepository = commentRepository;
    }

    public async Task ShowCommentList()
    {
        Console.Clear();
        Console.WriteLine("Showing list of comments");

        var comments = commentRepository.GetMany();

        if (!comments.Any())
        {
            Console.WriteLine("No comments available.\n");
        }
        else
        {
            foreach (var comment in comments)
            {
                Console.WriteLine($"ID: {comment.Id} \n" +
                                  $"Content: {comment.Content} \n" +
                                  $"Posted by: {comment.UserId} \n" +
                                  $"Posted on: {comment.PostId} \n" +
                                  $"Time: {comment.Date} \n");
            }
        }

        Console.WriteLine("Press any key to go back...");
        Console.ReadKey();
    }
}