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

        int postId = 0;

        while (true)
        {
            Console.WriteLine("Enter post ID to delete: ");
            if (int.TryParse(Console.ReadLine(), out postId))
            {
                break;
            }
            Console.WriteLine("Invalid ID format. Please enter a valid integer.");
        }
        
        while (true)
        {
            Console.WriteLine($"Are you sure you want to delete post with ID {postId}? (y/n): ");
            string? response = Console.ReadLine()?.ToLower();

            if (response == "y")
            {
                try
                {
                    await postRepository.DeleteAsync(postId);
                    Console.WriteLine("Post deleted successfully.\n");
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while deleting the post: {ex.Message}");
                    break;
                }
            }
            else if (response == "n")
            {
                Console.WriteLine("Post deletion canceled.");
                break;
            }

            Console.WriteLine("Invalid response. Please enter 'y' or 'n'.");
        }
        
        Console.WriteLine("Press any key to go back...");
        Console.ReadKey(); 
    }
}