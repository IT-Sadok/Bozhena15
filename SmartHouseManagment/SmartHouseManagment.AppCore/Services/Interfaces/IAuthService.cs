using Microsoft.AspNetCore.Identity;
using SmartHouseManagment.AppCore.Models.User;
using SmartHouseManagment.Domain.Entities;

namespace SmartHouseManagment.AppCore.Services.Interfaces;

public interface IAuthService
{
    Task<string?> RegisterUser(RegisterUserModel user);
    Task<string?> LoginUser(string email, string password);
}