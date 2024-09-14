using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class DeletePostView
{
    private readonly IPostRepository postRepository;
    
    public DeletePostView(IPostRepository postRepository)
    {
        this.postRepository = postRepository;
    }

    public async Task DeletePost()
    {
        Console.Clear();

        try
        {
            Console.WriteLine("Enter post ID to delete: ");
            if (!int.TryParse(Console.ReadLine(), out var postId))
            {
                Console.WriteLine("Invalid ID format. Please enter a valid integer.");
                return;
            }
            
            
            Console.WriteLine($"Are you sure you want to delete post with ID {postId}? (y/n): ");
            string? response = Console.ReadLine()?.ToLower();

            if (response == "y")
            {
                await postRepository.DeleteAsync(postId);
                Console.WriteLine("Post deleted successfully.\n");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while deleting the post: {ex.Message}");
        }
        
        Console.WriteLine("Press any key to go back...");
        Console.ReadKey(); 
    }
}
