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

    public bool idExists(string id)
    {
        int userId = int.Parse(id);
        
        for (int i = 0; i < userRepository.GetMany().Count(); i++)
        {
            if (userRepository.GetMany().FirstOrDefault(u => u.Id == userId) != null)
            {
                return true;
            }
        }
        return false;
    }

    public async Task DeleteUser()
    {
        Console.Clear();

        string? id = null;

        while (true)
        {
            Console.WriteLine("Enter id: ");
            id = Console.ReadLine();

            if (!string.IsNullOrEmpty(id) && idExists(id))
            {
                break;
            }

            Console.WriteLine("ID does not exist or null. Please try again.\n");
        }

        Console.WriteLine($"Are you sure you want to delete user with id {id}?: (y/n)");
        
        string? response = Console.ReadLine();
        
        if (response == "y")
        {
            await userRepository.DeleteAsync(int.Parse(id));
            Console.WriteLine("User deleted successfully.\n");
        }
        else
        {
            return;
        }
        
        Console.WriteLine("Press any key to go back...");
        Console.ReadKey();
    }
}