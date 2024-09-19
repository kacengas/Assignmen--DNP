using CLI.UI;
using RepositoryContracts;
using FileRepositories;

Console.WriteLine("Starting CLI App...");
IUserRepository userRepository = new UserFileRepository();
IPostRepository postRepository = new PostFileRepository();
ICommentRepository commentRepository = new CommentFileRepository();

CliApp cliApp = new CliApp(userRepository, postRepository, commentRepository);
await cliApp.StartAsync();