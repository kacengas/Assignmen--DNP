using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageUsers;

public class EditUserView
{
    private readonly IUserRepository userRepository;

    public EditUserView(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public bool IdExists(int userId)
    {
        return userRepository.GetMany().Any(u => u.Id == userId);
    }

    public async Task EditUser()
    {
        Console.Clear();

        User? user = null;
        string? newPassword = null;

        while (true)
        {
            Console.WriteLine("Enter user ID: ");
            string? id = Console.ReadLine();

            if (!string.IsNullOrEmpty(id) && IdExists(int.Parse(id)))
            {
                user = await userRepository.GetSingleAsync(int.Parse(id));
                break;
            }

            Console.WriteLine("Invalid or non-existent ID. Please try again.\n");
        }

        while (true)
        {
            Console.WriteLine("Enter new password: ");
            newPassword = Console.ReadLine();

            if (!string.IsNullOrEmpty(newPassword) && newPassword.Length >= 8)
            {
                break;
            }

            Console.WriteLine("Password must be at least 8 characters. Please try again.\n");
        }

        while (true)
        {
            Console.WriteLine("Enter new name: ");
            string? newName = Console.ReadLine();

            if (!string.IsNullOrEmpty(newName))
            {
                user.Username = newName;
                user.Password = newPassword;
                await userRepository.UpdateAsync(user);
                Console.WriteLine("User has been updated.\n");
                break;
            }

            Console.WriteLine("User name cannot be empty. Please try again.\n");
        }

        Console.WriteLine("Press any key to go back...");
        Console.ReadKey();
    }
}