using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageComments;

public class ShowCommentView
{
    private readonly ICommentRepository commentRepository;

    public ShowCommentView(ICommentRepository commentRepository)
    {
        this.commentRepository = commentRepository;
    }

    public async Task ShowComment()
    {
        Console.Clear();

        try
        {
            Console.WriteLine("Enter comment ID: ");
            if (!int.TryParse(Console.ReadLine(), out var commentId))
            {
                Console.WriteLine("Invalid ID format. Please enter a valid integer.");
                return;
            }

            var comment = await commentRepository.GetSingleAsync(commentId);

            if (comment == null)
            {
                Console.WriteLine($"Comment with ID {commentId} does not exist.");
                return;
            }

            Console.WriteLine($"ID: {comment.Id} \n" +
                              $"Content: {comment.Content} \n" +
                              $"Posted by: {comment.UserId} \n" +
                              $"Posted on: {comment.PostId} \n" +
                              $"Time: {comment.Date}");
            
            Console.WriteLine("\nPress any key to go back...");
            Console.ReadKey();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}