using Entities;
using RepositoryContracts;
using Enums;

namespace CLI.UI.ManageComments;

public class AddCommentReactionView
{
    private readonly ICommentRepository commentRepository;
    
    public AddCommentReactionView(ICommentRepository commentRepository)
    {
        this.commentRepository = commentRepository;
    }

    public async Task AddReaction()
    {
        Console.Clear();

        try
        {
            int commentId;
            while (true)
            {
                Console.WriteLine("Enter comment ID: ");
                if (int.TryParse(Console.ReadLine(), out commentId))
                {
                    break;
                }

                Console.WriteLine("Invalid comment ID format. Please enter a valid integer.");
            }

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

            string? reactionText;
            while (true)
            {
                Console.WriteLine("Enter reaction type: ");
                reactionText = Console.ReadLine();
                
                if (!string.IsNullOrEmpty(reactionText))
                {
                    break;
                }

                Console.WriteLine("Reaction text cannot be empty.");
            }

            await commentRepository.AddReaction(new CommentReaction
            {
                CommentId = commentId,
                UserId = userId,
                Type = Enum.TryParse<reactionType>(reactionText, true, out var reactionType) ? reactionType : throw new ArgumentException("Invalid reaction type")
                
            });

            Console.WriteLine("Reaction added successfully.");
            Console.WriteLine("Press any key to go back...");
            Console.ReadKey();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}