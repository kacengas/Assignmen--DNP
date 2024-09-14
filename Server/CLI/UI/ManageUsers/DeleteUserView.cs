using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageUsers;

public class DeleteUserView
{
    private readonly IUserRepository userRepository;

    public DeleteUserView(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public async Task DeleteUser()
    {
        Console.Clear();
        
        Console.WriteLine("Enter id: ");
        string? id = Console.ReadLine();

        Console.WriteLine($"Are you sure you want to delete user with id {id}?: (y/n)");
        string? response = Console.ReadLine();
        
        if (response == "y")
        {
            await userRepository.DeleteAsync(int.Parse(id));
        }
        else
        {
            return;
        }
        
        Console.WriteLine("User deleted \n");
    }
}