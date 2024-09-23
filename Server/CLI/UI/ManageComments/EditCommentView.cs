using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageComments;

public class EditCommentView
{
    private readonly ICommentRepository commentRepository;

    public EditCommentView(ICommentRepository commentRepository)
    {
        this.commentRepository = commentRepository;
    }

    public async Task EditComment()
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
                Console.WriteLine("Invalid ID format. Please enter a valid integer.");
            }

            var comment = await commentRepository.GetSingleAsync(commentId);

            string? newContent;
            while (true)
            {
                Console.WriteLine("Enter new content: ");
                newContent = Console.ReadLine();
                if (!string.IsNullOrEmpty(newContent))
                {
                    break;
                }
                Console.WriteLine("Content cannot be null or empty. Please try again.");
            }

            comment.Content = newContent;
            await commentRepository.UpdateAsync(comment);

            Console.WriteLine("Comment has been updated.\n");
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred while editing the comment: {e.Message}");
        }

        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }
}