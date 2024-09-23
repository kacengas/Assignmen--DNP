using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class EditPostView
{
    private readonly IPostRepository postRepository;

    public EditPostView(IPostRepository postRepository)
    {
        this.postRepository = postRepository;
    }

    public async Task EditPost()
    {
        Console.Clear();

        int postId = 0;
        string? newTitle = null;
        string? newContent = null;

        while (true)
        {
            Console.WriteLine("Enter post ID: ");
            if (int.TryParse(Console.ReadLine(), out postId))
            {
                try
                {
                    var post = await postRepository.GetSingleAsync(postId);
                    break;
                }
                catch (Exception)
                {
                    Console.WriteLine("Post with this ID does not exist. Please try again.\n");
                }
            }
            else
            {
                Console.WriteLine("Invalid ID format. Please enter a valid integer.");
            }
        }

        while (true)
        {
            Console.WriteLine("Enter new title: ");
            newTitle = Console.ReadLine();

            if (!string.IsNullOrEmpty(newTitle))
            {
                break;
            }

            Console.WriteLine("Title cannot be empty. Please try again.");
        }

        while (true)
        {
            Console.WriteLine("Enter new content: ");
            newContent = Console.ReadLine();

            if (!string.IsNullOrEmpty(newContent))
            {
                break;
            }

            Console.WriteLine("Content cannot be empty. Please try again.");
        }

        var postToUpdate = await postRepository.GetSingleAsync(postId);
        postToUpdate.Title = newTitle;
        postToUpdate.Content = newContent;

        await postRepository.UpdateAsync(postToUpdate);

        Console.WriteLine("Post has been updated.\n");
        Console.WriteLine("Press any key to go back...");
        Console.ReadKey();
    }
}
