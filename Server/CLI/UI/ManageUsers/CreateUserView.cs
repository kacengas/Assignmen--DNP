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

    public bool usernameFree(string name)
    {
        for (int i = 0; i < userRepository.GetMany().Count(); i++)
        {
            if (name.Equals(userRepository.GetMany().ElementAt(i).Username))
            {
                return false;
            }
        }
        return true;
    }

    public async Task CreateUser()
    {
        Console.Clear();

        string? name = null;
        string? password = null;
        
        while (true)
        {
            Console.WriteLine("Enter user name: ");
            name = Console.ReadLine();

            if (!string.IsNullOrEmpty(name) && usernameFree(name))
            {
                break;
            }
            Console.WriteLine("Username taken or empty. Please try again.\n");
        }
        
        while (true)
        {
            Console.WriteLine("Enter password: ");
            password = Console.ReadLine();

            if (!string.IsNullOrEmpty(password) && password.Length >= 8)
            {
                break;
            }
            Console.WriteLine("Password must be at least 8 characters. Please try again.\n");
        }
        
        User user = new User
        {
            Username = name,
            Password = password
        };
        await userRepository.AddAsync(user);

        Console.WriteLine("User created successfully.\n");

        Console.WriteLine("Press any key to go back...");
        Console.ReadKey();
    }

}