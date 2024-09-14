using CLI.UI.ManagePosts;
using RepositoryContracts;
using CLI.UI.ManageUsers;

namespace CLI.UI;

public class CliApp
{
    private readonly IUserRepository userRepository;
    private readonly IPostRepository postRepository;
    private readonly ICommentRepository commentRepository;

    public CliApp(IUserRepository userRepository, IPostRepository postRepository, ICommentRepository commentRepository)
    {
        this.userRepository = userRepository;
        this.postRepository = postRepository;
        this.commentRepository = commentRepository;
    }

    public async Task StartAsync()
    {
        while (true)
        { 
            Console.Clear();
            
            Console.WriteLine("--- Main menu ---");
            Console.WriteLine("1. Manage users");
            Console.WriteLine("2. Manage posts");
            Console.WriteLine("3. Manage comments");
            Console.WriteLine("0. Exit");
            Console.Write("Enter option: ");
            string? input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    await ManageUsersAsync();
                    break;
                case "2":
                    await ManagePostsAsync();
                    break;
                case "3":
                    await ManageCommentsAsync();
                    break;
                case "0":
                    break;
                default:
                    Console.WriteLine("\nInvalid option. Please try again.\n");
                    break;
            }
        }
    }

    private async Task ManageUsersAsync()
    {
        Console.Clear();
        
        CreateUserView createUserView = new CreateUserView(userRepository);
        DeleteUserView deleteUserView = new DeleteUserView(userRepository);
        EditUserView editUserView = new EditUserView(userRepository);
        ShowUserView showUserView = new ShowUserView(userRepository);
        UserListView userListView = new UserListView(userRepository);

        while (true)
        {
            Console.WriteLine("--- Manage Users ---");
            Console.WriteLine("1. Create user");
            Console.WriteLine("2. Edit user");
            Console.WriteLine("3. Delete user");
            Console.WriteLine("4. Show user");
            Console.WriteLine("5. Show list of users");
            Console.WriteLine("0. Back");
            Console.Write("Enter option: ");
            string? option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    await createUserView.CreateUser();
                    break;
                case "2":
                    await editUserView.EditUser();
                    break;
                case "3":
                    await deleteUserView.DeleteUser();        
                    break;
                case "4":
                    await showUserView.ShowUser();
                    break;
                case "5":
                    await userListView.ShowUserList();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("\nInvalid option. Please try again.\n");
                    break;
            }
        }
    }

    private async Task ManagePostsAsync()
    {
        CreatePostView createPostView = new CreatePostView(postRepository);
        EditPostView editPostView = new EditPostView(postRepository);
        DeletePostView deletePostView = new DeletePostView(postRepository);
        ShowPostView showPostView = new ShowPostView(postRepository);
        PostListView postListView = new PostListView(postRepository);
        
        Console.WriteLine("--- Manage Posts ---");
        Console.WriteLine("1. Create post");
        Console.WriteLine("2. Edit post");
        Console.WriteLine("3. Delete post");
        Console.WriteLine("4. Show post");
        Console.WriteLine("5. Show list of posts");
        Console.WriteLine("0. Back");
        Console.Write("Enter option: ");
        string? option = Console.ReadLine();

        switch (option)
        {
            case "1":
                await createPostView.CreatePost();
                break;
            case "2":
                await editPostView.EditUser();
                break;
            case "3":
                await deletePostView.DeletePost();        
                break;
            case "4":
                await showPostView.ShowPost();
                break;
            case "5":
                await postListView.ShowPostList();
                break;
            case "0":
                return;
            default:
                Console.WriteLine("\nInvalid option. Please try again.\n");
                break;
        }
    }

    private async Task ManageCommentsAsync()
    {
        
    }
}
