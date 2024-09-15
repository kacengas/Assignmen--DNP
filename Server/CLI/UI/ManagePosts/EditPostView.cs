using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class EditPostView
{
    private readonly IPostRepository postRepository;

    public EditPostView(IPostRepository postRepository)
    {
        this.postRepository = postRepository;
    }

    public async Task EditUser()
    {
        Console.Clear();

        try
        {
            Console.WriteLine("Enter post id: ");
            if (!int.TryParse(Console.ReadLine(), out int postId))
            {
                Console.WriteLine("Invalid ID format. Please enter a valid integer.");
                return;
            }

            var post = await postRepository.GetSingleAsync(postId);

            Console.WriteLine("Enter new title: ");
            var newTitle = Console.ReadLine();

            Console.WriteLine("Enter new content: ");
            var newContent = Console.ReadLine();
            
            if (string.IsNullOrEmpty(newTitle) || string.IsNullOrEmpty(newContent))
            {
                throw new Exception("Title and content cannot be null or empty \n");
            }

            post.Title = newTitle;
            post.Content = newContent;

            await postRepository.UpdateAsync(post);

            Console.WriteLine("User has been updated \n");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while editing the post: {ex.Message}");
        }
        
        Console.WriteLine("Press any key to go back...");
        Console.ReadKey();
    }
}