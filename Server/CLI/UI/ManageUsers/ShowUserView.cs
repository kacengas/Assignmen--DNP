using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageUsers;

public class ShowUserView
{
    private readonly IUserRepository userRepository;

    public ShowUserView(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public async Task ShowUser()
    {
        Console.Clear();
        
        Console.WriteLine("Enter id: ");
        var userId = int.Parse(Console.ReadLine());

        var user = await userRepository.GetSingleAsync(userId);
        
        Console.WriteLine($"ID: {user.Id} \n" +
                          $"Name: {user.Username} \n" +
                          $"Password: {user.Password} \n" );
        
        Console.WriteLine("Press any key to go back...");
        Console.ReadKey();
    }
}