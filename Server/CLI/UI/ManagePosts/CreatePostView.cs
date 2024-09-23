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

        int userId = 0;
        string? title = null;
        string? description = null;

        while (true)
        {
            Console.WriteLine("Enter user ID: ");
            if (int.TryParse(Console.ReadLine(), out userId))
            {
                break;
            }
            Console.WriteLine("Invalid user ID format. Please enter a valid integer.");
        }

        while (true)
        {
            Console.WriteLine("Enter title: ");
            title = Console.ReadLine();
            if (!string.IsNullOrEmpty(title))
            {
                break;
            }
            Console.WriteLine("Title cannot be empty. Please try again.");
        }

        while (true)
        {
            Console.WriteLine("Enter description: ");
            description = Console.ReadLine();
            if (!string.IsNullOrEmpty(description))
            {
                break;
            }
            Console.WriteLine("Description cannot be empty. Please try again.");
        }

        Post post = new Post(userId, title, description);
        await postRepository.AddAsync(post);

        Console.WriteLine("Post created successfully.\n");
        Console.WriteLine("Press any key to go back...");
        Console.ReadKey();
    }
}