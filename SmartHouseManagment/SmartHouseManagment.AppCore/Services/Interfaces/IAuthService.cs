using SmartHouseManagment.AppCore.Models;
using SmartHouseManagment.AppCore.Models.User;

namespace SmartHouseManagment.AppCore.Services.Interfaces;

public interface IAuthService
{
    Task<ResultModel<string>> RegisterUserAsync(RegisterUserModel registerUser, CancellationToken cancellationToken);
    Task<ResultModel<string>> LoginUserAsync(LoginUserModel loginUser, CancellationToken cancellationToken);
}