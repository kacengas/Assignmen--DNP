using DTOs;

namespace BlazorApp1.Services;

public interface IUserService
{
    Task<CreateUserDTO> AddUserAsync(CreateUserDTO createUserDto);
    Task<UserDTO> GetUserAsync(int id);
    Task GetByUsernameAsync(string username);
}