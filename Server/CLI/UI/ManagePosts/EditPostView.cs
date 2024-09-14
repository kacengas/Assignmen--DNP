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
        
        Console.WriteLine("Enter post id: ");
        var postId = Console.ReadLine();

        var post = await postRepository.GetSingleAsync(int.Parse(postId));
        
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
}