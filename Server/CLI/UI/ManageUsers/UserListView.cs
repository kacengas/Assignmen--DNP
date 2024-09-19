using RepositoryContracts;

namespace CLI.UI.ManageUsers;

public class UserListView
{
    private readonly IUserRepository userRepository;

    public UserListView(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public async Task ShowUserList()
    {
        Console.Clear();
        Console.WriteLine("Showing list of users");

        var users = userRepository.GetMany();

        foreach (var user in users)
        {
            Console.WriteLine(
                $"ID: {user.Id} \n" +
                $"Name: {user.Username} \n" +
                $"Password: {user.Password} \n");
        }
        
        Console.WriteLine("Press any key to go back...");
        Console.ReadKey();
    }
}