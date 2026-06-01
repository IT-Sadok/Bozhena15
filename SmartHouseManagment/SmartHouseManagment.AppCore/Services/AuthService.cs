using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SmartHouseManagment.AppCore.Extensions;
using SmartHouseManagment.AppCore.Extensions.Mapper;
using SmartHouseManagment.AppCore.Models.User;
using SmartHouseManagment.AppCore.Services.Interfaces;
using SmartHouseManagment.Domain.Entities;

namespace SmartHouseManagment.AppCore.Services;

public class AuthService(
    UserManager<User> userManager,
    ITokenService tokenService,
    ILogger<AuthService> logger) : IAuthService
{
    public async Task<string?> RegisterUser(RegisterUserModel registerUser)
    {
        var user = registerUser.ToEntity();

        var result = await userManager.CreateAsync(user, registerUser.Password);
        
        if (!result.Succeeded)
        {
            logger.LogError("{Service}: {Method}: Failed to register user - {email}.",
                nameof(AuthService),
                nameof(RegisterUser),
                user.Email);
            
            return null;
        } 
        
        var claims = GetClaims(user, UserRole.User);
        
        await userManager.AddToRoleAsync(user, UserRole.User.ToEnumDescription());
        await userManager.AddClaimsAsync(user, claims);
        
        return tokenService.GenerateToken(user, claims);
    }

    public async Task<string?> LoginUser(string email, string password)
    {
        var user = await userManager.FindByEmailAsync(email);
        
        if(user is null)
        {
            logger.LogError("{Service}:{Method}: User not found - {email}.",
                nameof(AuthService), 
                nameof(LoginUser), 
                email);
            
            return null;
        }

        var isValidPassword = await userManager.CheckPasswordAsync(user, password);

        if (!isValidPassword)
        {
            logger.LogError("{Service}: {Method}: Password is incorrect - {email}.",
                nameof(AuthService),
                nameof(LoginUser),
                email);
            
            return null;
        }        
        
        var claims = await userManager.GetClaimsAsync(user);
        
        return tokenService.GenerateToken(user, claims);       
    }
    
    private static List<Claim> GetClaims(User user, UserRole role)
        =>
        [
            new(ClaimTypes.NameIdentifier, user.Id),
            new(ClaimTypes.Name, user.UserName),
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.Role, role.ToEnumDescription())
        ];
}