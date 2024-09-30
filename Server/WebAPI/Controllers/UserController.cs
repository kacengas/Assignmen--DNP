using DTOs;
using Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController
{
    private readonly IUserRepository userRepository;
    
    public UserController(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }
    
    // POST https://localhost:7276/User
    [HttpPost]
    public async Task<IResult> CreateUser([FromBody]CreateUserDto request)
    {
        User user = new User
        {
            Username = request.Username,
            Password = request.Password
        };
        
        await userRepository.AddAsync(user);
        return Results.Created($"users/{user.Id}", user);
    }
    
    // GET https://localhost:7276/User/{id}
    [HttpGet("{id}")]
    public async Task<IResult> GetSingleUser([FromRoute]int id)
    {
        User result = await userRepository.GetSingleAsync(id);
        return Results.Ok(result);
    }
    
    // DELETE https://localhost:7276/User/{id}
    [HttpDelete("{id}")]
    public async Task<IResult> DeleteUser([FromRoute]int id)
    {
        User user = await userRepository.GetSingleAsync(id);
        if (user == null)
        {
            return Results.NotFound();
        }
        
        userRepository.DeleteAsync(user.Id);
        return Results.NoContent();
    }
    
    // PUT https://localhost:7276/User/{id}
    [HttpPut("{id}")]
    public async Task<IResult> UpdateUser([FromRoute]int id, [FromBody]UpdateUserDto request)
    {
        User user = await userRepository.GetSingleAsync(id);
        if (user == null)
        {
            return Results.NotFound();
        }
        
        user.Username = request.Username;
        user.Password = request.Password;
        
        await userRepository.UpdateAsync(user);
        return Results.Ok(user);
    }
    
    //GET https://localhost:7276/User
    [HttpGet]
    public IResult GetUsers()
    {
        return Results.Ok(userRepository.GetMany());
    }
}