using SmartHouseManagment.AppCore.Models;
using SmartHouseManagment.AppCore.Models.User;

namespace SmartHouseManagment.AppCore.Services.Interfaces;

public interface IAuthService
{
    Task<ResultModel<string>> RegisterUserAsync(RegisterUserModel user);
    Task<ResultModel<string>> LoginUserAsync(string email, string password);
}