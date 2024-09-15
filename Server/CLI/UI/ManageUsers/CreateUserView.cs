using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageUsers;

public class CreateUserView
{
    private readonly IUserRepository userRepository;

    public CreateUserView(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public async Task CreateUser()
    {
        Console.Clear();
        
        Console.WriteLine("Enter user name: ");
        string? name = Console.ReadLine();

        if (string.IsNullOrEmpty(name))
        {
            Console.WriteLine("User name cannot be empty");
            return;
        }

        Console.WriteLine("Enter password: ");
        string? password = Console.ReadLine();

        if (string.IsNullOrEmpty(password) || password.Length < 8)
        {
            Console.WriteLine("Password must be at least 8 characters \n");
            return;
        }

        User user = new User(name, password);
        await userRepository.AddAsync(user); 

        Console.WriteLine("User created \n");
        
        Console.WriteLine("Press any key to go back...");
        Console.ReadKey();
    }
}