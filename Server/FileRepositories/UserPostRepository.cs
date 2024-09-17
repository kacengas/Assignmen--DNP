using System.Reflection.Metadata;
using System.Text.Json;
using Entities;

namespace FileRepositories;

public class UserPostRepository
{
    private readonly string filePath = "comments.json";

    public UserPostRepository()
    {
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "[]");
        }
    }

    public async Task<List<User>> ReadData()
    {
        string usersAsJson = await File.ReadAllTextAsync(filePath);
        return JsonSerializer.Deserialize<List<User>>(usersAsJson) ?? new List<User>();
    }
    
    private async Task WriteData(List<User> users)
    {
        string usersAsJson = JsonSerializer.Serialize(users);
        await File.WriteAllTextAsync(filePath, usersAsJson);
    }
    
    public async Task<User> AddAsync(User user)
    {
        List<User> users = await ReadData();
        int maxId = users.Count > 0 ? users.Max(u => u.Id) : 1;
        user.Id = maxId + 1;
        users.Add(user);
        await WriteData(users);
        return user;
    }

    public async Task<User> DeleteAsync(int id)
    {
        List<User> users = await ReadData();
        User? userToDelete = users.Find(u => u.Id == id);
        
        if (userToDelete == null)
        {
            throw new InvalidOperationException(
                $"User with ID '{id}' not found'");
        }
        
        users.Remove(userToDelete);
        await WriteData(users);
        return userToDelete;
    }

    public async Task<User> UpdateAsync(User user)
    {
        
    }
}