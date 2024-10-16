using CLI.UI.ManagePosts;
using RepositoryContracts;
using CLI.UI.ManageUsers;
using CLI.UI.ManageComments;

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
            Console.Clear();
            
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
        Console.Clear();
        
        CreatePostView createPostView = new CreatePostView(postRepository);
        EditPostView editPostView = new EditPostView(postRepository);
        DeletePostView deletePostView = new DeletePostView(postRepository);
        ShowPostView showPostView = new ShowPostView(postRepository, commentRepository, userRepository);
        PostListView postListView = new PostListView(postRepository);

        while (true)
        {
            Console.Clear();
            
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
                    await editPostView.EditPost();
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
                case "6":
                    await ManagePostReactionsAsync();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("\nInvalid option. Please try again.\n");
                    break;
            }
        }
    }

    private async Task ManageCommentsAsync()
    {
        Console.Clear();
        
        CreateCommentView createCommentView = new CreateCommentView(commentRepository);
        EditCommentView editCommentView = new EditCommentView(commentRepository);
        DeleteCommentView deleteCommentView = new DeleteCommentView(commentRepository);
        ShowCommentView showCommentView = new ShowCommentView(commentRepository);
        CommentListView postCommentView = new CommentListView(commentRepository);
        AddCommentReactionView addCommentReactionView = new AddCommentReactionView(commentRepository);
        CommentReactionListView commentReactionListView = new CommentReactionListView(commentRepository);

        while (true)
        {
            Console.Clear();
            
            Console.WriteLine("--- Manage Comments ---");
            Console.WriteLine("1. Create comment");
            Console.WriteLine("2. Edit comment");
            Console.WriteLine("3. Delete comment");
            Console.WriteLine("4. Show comment");
            Console.WriteLine("5. Show list of comments");
            Console.WriteLine("6. Manage comment reactions");
            Console.WriteLine("0. Back");
            Console.Write("Enter option: ");
            string? option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    await createCommentView.CreateComment();
                    break;
                case "2":
                    await editCommentView.EditComment();
                    break;
                case "3":
                    await deleteCommentView.DeleteComment();        
                    break;
                case "4":
                    await showCommentView.ShowComment();
                    break;
                case "5":
                    await postCommentView.ShowCommentList();
                    break;
                case "6":
                    await ManageCommentReactionsAsync();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("\nInvalid option. Please try again.\n");
                    break;
            }
        }
    }
    
    private async Task ManageCommentReactionsAsync()
    {
        Console.Clear();
        
        AddCommentReactionView addCommentReactionView = new AddCommentReactionView(commentRepository);
        CommentReactionListView commentReactionListView = new CommentReactionListView(commentRepository);
        EditCommentReactionView EditCommentReactionView = new EditCommentReactionView(commentRepository);
        DeleteCommentReactionView DeleteCommentReactionView = new DeleteCommentReactionView(commentRepository);
        ShowCommentReactionView ShowCommentReactionView = new ShowCommentReactionView(commentRepository);

        while (true)
        {
            Console.Clear();
            
            Console.WriteLine("--- Manage Comment Reactions ---");
            Console.WriteLine("1. Create reaction");
            Console.WriteLine("2. Edit reaction");
            Console.WriteLine("3. Delete reaction");
            Console.WriteLine("4. Show reaction");
            Console.WriteLine("5. Show list of reactions");
            Console.WriteLine("0. Back");
            Console.Write("Enter option: ");
            string? option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    await addCommentReactionView.AddReaction();
                    break;
                case "2":
                    await EditCommentReactionView.EditReaction();
                    break;
                case "3":
                    await DeleteCommentReactionView.DeleteReaction();
                    break;
                case "4":
                    await ShowCommentReactionView.ShowReaction();
                    break;
                case "5":
                    await commentReactionListView.ShowList();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("\nInvalid option. Please try again.\n");
                    break;
            }
        }
    }

    public async Task ManagePostReactionsAsync()
    {
        Console.Clear();
        
        AddPostReactionView addPostReactionView = new AddPostReactionView(postRepository);
        PostReactionListView postReactionListView = new PostReactionListView(postRepository);
        EditPostReactionView editPostReactionView = new EditPostReactionView(postRepository);
        DeletePostReactionView deletePostReactionView = new DeletePostReactionView(postRepository);
        ShowPostReactionView showPostReactionView = new ShowPostReactionView(postRepository);
        
        while (true)
        {
            Console.Clear();
            
            Console.WriteLine("--- Manage Post Reactions ---");
            Console.WriteLine("1. Create reaction");
            Console.WriteLine("2. Edit reaction");
            Console.WriteLine("3. Delete reaction");
            Console.WriteLine("4. Show reaction");
            Console.WriteLine("5. Show list of reactions");
            Console.WriteLine("0. Back");
            Console.Write("Enter option: ");
            string? option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    await addPostReactionView.AddReaction();
                    break;
                case "2":
                    await editPostReactionView.EditReaction();
                    break;
                case "3":
                    await deletePostReactionView.DeleteReaction();
                    break;
                case "4":
                    await showPostReactionView.ShowReaction();
                    break;
                case "5":
                    await postReactionListView.ShowList();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("\nInvalid option. Please try again.\n");
                    break;
            }
        }
    }
}
