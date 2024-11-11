using System.Security.Claims;
using System.Text.Json;
using DTOs;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlazorApp1.Auth;

public class SimpleAuthProvider : AuthenticationStateProvider
{
    private readonly HttpClient httpClient;
    private ClaimsPrincipal currentClaimsPrincipal;

    public SimpleAuthProvider(HttpClient httpClient)
    {
        this.httpClient = httpClient;
        this.currentClaimsPrincipal = new ClaimsPrincipal(); // Initial anonymous user
    }
    
    public async Task Login(string userName, string password) 
    { 
        try
        {
            HttpResponseMessage response = await httpClient.PostAsJsonAsync("auth/login", new LoginDTO(userName, password));
            
            string content = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Login API response status: {response.StatusCode}");
            Console.WriteLine($"Login API response content: {content}");
            
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Login failed: {content}");
            } 
            
            UserDTO? userDto = JsonSerializer.Deserialize<UserDTO>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (userDto == null)
            {
                throw new Exception("Failed to deserialize login response.");
            }

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, userDto.UserName), 
                new Claim("Id", userDto.Id.ToString())
            };
            
            ClaimsIdentity identity = new ClaimsIdentity(claims, "apiauth");
            currentClaimsPrincipal = new ClaimsPrincipal(identity); 
            
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(currentClaimsPrincipal)));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception during login: {ex.Message}");
            throw; // Re-throw to keep the stack trace in case of additional logging
        }
    }
    
    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        // Ensure we return the current authenticated state, even if anonymous
        return Task.FromResult(new AuthenticationState(currentClaimsPrincipal ?? new ClaimsPrincipal()));
    }

    public void Logout()
    {
        currentClaimsPrincipal = new ClaimsPrincipal(); // Reset to an anonymous user
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(currentClaimsPrincipal)));
    }
}
