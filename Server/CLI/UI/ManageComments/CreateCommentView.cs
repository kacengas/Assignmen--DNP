using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageComments;

public class CreateCommentView
{
    private readonly ICommentRepository commentRepository;

    public CreateCommentView(ICommentRepository commentRepository)
    {
        this.commentRepository = commentRepository;
    }

    public async Task CreateComment()
    {
        Console.Clear();

        try
        {
            Console.WriteLine($"Enter user ID");
            if (!int.TryParse(Console.ReadLine(), out var userId))
            {
                Console.WriteLine("Invalid user ID format. Please enter a valid integer.");
                return;
            }
            
            Console.WriteLine($"Enter post ID");
            if (!int.TryParse(Console.ReadLine(), out var postId))
            {
                Console.WriteLine("Invalid user ID format. Please enter a valid integer.");
                return;
            }
            
            Console.WriteLine($"Enter comment text");
            string? commentText = Console.ReadLine();
            if (string.IsNullOrEmpty(commentText))
            {
                Console.WriteLine("Description cannot be empty.");
                return;
            }

            await commentRepository.AddAsync(new Comment(commentText, userId, postId));
            
            Console.WriteLine("Comment added. \n");
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred while creating the comment: {e.Message}");
        }
        
        Console.WriteLine("Press any key to go back...");
        Console.ReadKey();
    }
}