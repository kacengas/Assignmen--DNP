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
            Console.WriteLine("Enter comment id: ");
            if (!int.TryParse(Console.ReadLine(), out int postId))
            {
                Console.WriteLine("Invalid ID format. Please enter a valid integer.");
                return;
            }

            var post = await commentRepository.GetSingleAsync(postId);
            
            Console.WriteLine("Enter new content: ");
            var newContent = Console.ReadLine();
            
            if (string.IsNullOrEmpty(newContent))
            {
                throw new Exception("Title cannot be null or empty \n");
            }
            
            post.Content = newContent;

            await commentRepository.UpdateAsync(post);

            Console.WriteLine("Comment has been updated \n");
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred while editing the comment: {e.Message}");
        }
        
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }
}