using System.Reflection.Metadata;
using System.Text.Json;
using Entities;
using RepositoryContracts;

namespace FileRepositories;

public class UserFileRepository : IUserRepository
{
    private readonly string filePath = "comments.json";

    public UserFileRepository()
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
        int maxId = users.Count > 0 ? users.Max(u => u.Id) : 0;
        user.Id = maxId + 1;
        users.Add(user);
        await WriteData(users);
        return user;
    }

    public async Task DeleteAsync(int id)
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
    }

    public async Task UpdateAsync(User user)
    {
        List<User> users = await ReadData();
        User? userToUpdate = users.Find(u => u.Id == user.Id);
        
        if (userToUpdate == null)
        {
            throw new InvalidOperationException(
                $"User with ID '{user.Id}' not found'");
        }
        
        users.Remove(userToUpdate);
        users.Add(user);
        await WriteData(users);
    }

    public async Task<User> GetSingleAsync(int id)
    {
        List<User> users = await ReadData();
        User? userToGet = users.Find(u => u.Id == id);
        
        if (userToGet is null)
        {
            throw new InvalidOperationException(
                $"User with ID '{id}' not found");
        }

        return userToGet;
    }
    
    public IQueryable<User> GetMany()
    {
        string usersAsJson = File.ReadAllTextAsync(filePath).Result;
        List<User> users = JsonSerializer.Deserialize<List<User>>(usersAsJson) !;
        return users.AsQueryable();
    }
}