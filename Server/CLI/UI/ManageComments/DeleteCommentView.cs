using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageComments;

public class DeleteCommentView
{
    private readonly ICommentRepository commentRepository;

    public DeleteCommentView(ICommentRepository commentRepository)
    {
        this.commentRepository = commentRepository;
    }

    public async Task DeleteComment()
    {
        Console.Clear();

        try
        {
            Console.WriteLine("Enter comment ID to delete: ");
            if (!int.TryParse(Console.ReadLine(), out var commentId))
            {
                Console.WriteLine("Invalid ID format. Please enter a valid integer.");
                return;
            }
            
            
            Console.WriteLine($"Are you sure you want to delete comment with ID {commentId}? (y/n): ");
            string? response = Console.ReadLine()?.ToLower();

            if (response == "y")
            {
                await commentRepository.DeleteAsync(commentId);
                Console.WriteLine("Comment deleted successfully.\n");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred while deleting the comment: {e.Message}");
        }
        
        Console.WriteLine("Press any key to go back...");
        Console.ReadKey();
    }
}