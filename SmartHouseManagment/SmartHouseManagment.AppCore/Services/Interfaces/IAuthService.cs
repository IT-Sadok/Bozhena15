using SmartHouseManagment.AppCore.Models;
using SmartHouseManagment.AppCore.Models.User;

namespace SmartHouseManagment.AppCore.Services.Interfaces;

public interface IAuthService
{
    Task<ResultModel<RegisterUserResponse>> RegisterUserAsync(RegisterUserModel registerUser, CancellationToken cancellationToken);
    Task<ResultModel<LoginUserResponse>> LoginUserAsync(LoginUserModel loginUser, CancellationToken cancellationToken);
}