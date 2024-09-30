using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class ShowPostView
{
    private readonly IPostRepository postRepository;
    private readonly ICommentRepository commentRepository;
    private readonly IUserRepository userRepository;

    public ShowPostView(IPostRepository postRepository, ICommentRepository commentRepository, IUserRepository userRepository)
    {
        this.postRepository = postRepository;
        this.commentRepository = commentRepository;
        this.userRepository = userRepository;
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
            
            var comments = commentRepository.GetMany().ToList();
        
            if (comments.Any())
            {
                Console.WriteLine("Comments: \n");
                foreach (var comment in comments)
                {
                    var user = await userRepository.GetSingleAsync(comment.UserId);   
                    Console.WriteLine($"{user.Username}: {comment.Content}");
                }
            }
            else
            {
                Console.WriteLine("No comments yet.");
            }
            
            Console.WriteLine("\nPress any key to go back...");
            Console.ReadKey();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

}