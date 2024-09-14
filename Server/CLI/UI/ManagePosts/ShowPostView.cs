using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class ShowPostView
{
    private readonly IPostRepository postRepository;

    public ShowPostView(IPostRepository postRepository)
    {
        this.postRepository = postRepository;
    }

    public async Task ShowPost()
    {
        Console.Clear();

        try
        {
            Console.WriteLine("Enter post ID: ");
            if (!int.TryParse(Console.ReadLine(), out var postId))
            {
                Console.WriteLine("Invalid ID format. Please enter a valid integer.");
                return;
            }

            var post = await postRepository.GetSingleAsync(postId);

            if (post == null)
            {
                Console.WriteLine($"Post with ID {postId} does not exist.");
                return;
            }

            Console.WriteLine($"ID: {post.Id} \n" +
                              $"Title: {post.Title} \n" +
                              $"Content: {post.Content} \n" +
                              $"Posted by: {post.UserId} \n" +
                              $"Time: {post.Date}");
            
            Console.WriteLine("\nPress any key to go back...");
            Console.ReadKey();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

}