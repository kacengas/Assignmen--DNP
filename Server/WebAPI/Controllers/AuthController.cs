using DTOs;
using Microsoft.AspNetCore.Identity.Data;
using RepositoryContracts;

namespace WebAPI.Controllers;

using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public AuthController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDTO loginRequest)
    {
        var user = await _userRepository.GetByUsernameAsync(loginRequest.Username);
        
        if (user == null || user.Password != loginRequest.Password)
        {
            return Unauthorized("Invalid username or password.");
        }

        var userDto = new UserDTO()
        { 
            Id = user.Id,
            UserName = user.Username
        };

        return Ok(userDto);
    }
}