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
            int userId;
            while (true)
            {
                Console.WriteLine("Enter user ID: ");
                if (int.TryParse(Console.ReadLine(), out userId))
                {
                    break;
                }
                Console.WriteLine("Invalid user ID format. Please enter a valid integer.");
            }
            
            int postId;
            while (true)
            {
                Console.WriteLine("Enter post ID: ");
                if (int.TryParse(Console.ReadLine(), out postId))
                {
                    break;
                }
                Console.WriteLine("Invalid post ID format. Please enter a valid integer.");
            }

            string? commentText;
            while (true)
            {
                Console.WriteLine("Enter comment text: ");
                commentText = Console.ReadLine();
                if (!string.IsNullOrEmpty(commentText))
                {
                    break;
                }
                Console.WriteLine("Comment text cannot be empty.");
            }

            await commentRepository.AddAsync(new Comment(commentText, userId, postId));
            
            Console.WriteLine("Comment added successfully.\n");
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred while creating the comment: {e.Message}");
        }

        Console.WriteLine("Press any key to go back...");
        Console.ReadKey();
    }
}