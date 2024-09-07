namespace Entities;

public class User
{
    public User(int ID, string username, string password)
    {
        Id = ID;
        Username = username;
        Password = password;
    }

    public int Id{get;set;}

    public string Username{get;set;}

    public string Password{get;set;}
}