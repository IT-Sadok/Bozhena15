using System.Security.Claims;
using SmartHouseManagment.AppCore.Models.User;
using SmartHouseManagment.Domain.Entities;

namespace SmartHouseManagment.AppCore.Services.Interfaces;

public interface ITokenService
{
    string GenerateToken(User user, IList<Claim> claims);
}