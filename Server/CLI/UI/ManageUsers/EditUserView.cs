using RepositoryContracts;

namespace CLI.UI.ManageUsers;

public class EditUserView
{
    private readonly IUserRepository userRepository;

    public EditUserView(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public async Task EditUser()
    {
        Console.Clear();
        
        Console.WriteLine("Enter id: ");
        var userId = Console.ReadLine();

        var user = await userRepository.GetSingleAsync(int.Parse(userId));
        
        Console.WriteLine("Enter new name: ");
        var newName = Console.ReadLine();
        
        Console.WriteLine("Enter new password: ");
        var newPassword = Console.ReadLine();

        if (string.IsNullOrEmpty(newName) || string.IsNullOrEmpty(newPassword))
        {
            throw new Exception("Username and password cannot be null or empty \n");
        }

        user.Username = newName;
        user.Password = newPassword;

        await userRepository.UpdateAsync(user);
        
        Console.WriteLine("User has been updated \n");
    }
}