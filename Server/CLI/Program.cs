// See https://aka.ms/new-console-template for more information

using CLI.UI;
using InMemoryRepositories;
using RepositoryContracts;

Console.WriteLine("Starting CLI App...");
IUserRepository userRepository = new UserInMemoryRepositories();
IPostRepository postRepository = new PostInMemoryRepositories();
ICommentRepository commentRepository = new CommentInMemoryRepositories();

CliApp cliApp = new CliApp(userRepository, postRepository, commentRepository);
await cliApp.StartAsync();