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

    public bool IdExists(int userId)
    {
        return userRepository.GetMany().Any(u => u.Id == userId);
    }

    public async Task ShowUser()
    {
        Console.Clear();

        while (true)
        {
            Console.WriteLine("Enter user ID: ");
            string? inputId = Console.ReadLine();

            if (int.TryParse(inputId, out int userId) && IdExists(userId))
            {
                var user = await userRepository.GetSingleAsync(userId);

                Console.WriteLine($"ID: {user.Id} \n" +
                                  $"Name: {user.Username} \n" +
                                  $"Password: {user.Password} \n");
                break;
            }

            Console.WriteLine("Invalid or non-existent ID. Please try again.\n");
        }

        Console.WriteLine("Press any key to go back...");
        Console.ReadKey();
    }
}