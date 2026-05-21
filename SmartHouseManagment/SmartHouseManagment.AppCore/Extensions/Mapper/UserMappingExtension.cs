using Microsoft.AspNetCore.Identity;
using SmartHouseManagment.AppCore.Dtos;
using SmartHouseManagment.Domain.Entities;

namespace SmartHouseManagment.AppCore.Extensions.Mapper;

public static class UserMappingExtension
{
    public static User ToEntity(this RegisterUserDto registerUserDto, IPasswordHasher<User> hasher)
    {
        var user = new User()
        {
            Email = registerUserDto.Email,
            Name = registerUserDto.Name,
            Role = UserRole.User
        };
        
        user.PasswordHash = hasher.HashPassword(user, registerUserDto.Password);
        
        return user;
    }
}