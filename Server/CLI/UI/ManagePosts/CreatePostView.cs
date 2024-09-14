using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class CreatePostView
{
    private readonly IPostRepository postRepository;

    public CreatePostView(IPostRepository postRepository)
    {
        this.postRepository = postRepository;
    }

    public async Task CreatePost()
    {
        Console.Clear();

        try
        {
            Console.WriteLine("Enter user ID: ");
            if (!int.TryParse(Console.ReadLine(), out var userId))
            {
                Console.WriteLine("Invalid user ID format. Please enter a valid integer.");
                return;
            }

            Console.WriteLine("Enter title: ");
            string? title = Console.ReadLine();
            if (string.IsNullOrEmpty(title))
            {
                Console.WriteLine("Title cannot be empty.");
                return;
            }
            
            Console.WriteLine("Enter description: ");
            string? description = Console.ReadLine();
            if (string.IsNullOrEmpty(description))
            {
                Console.WriteLine("Description cannot be empty.");
                return;
            }

            Post post = new Post(userId, title, description);

            await postRepository.AddAsync(post);

            Console.WriteLine("Post created successfully.\n");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while creating the post: {ex.Message}");
        }

        Console.WriteLine("Press any key to go back...");
        Console.ReadKey();
    }
}