using Microsoft.AspNetCore.Identity;
using SmartHouseManagment.AppCore.Models.User;
using SmartHouseManagment.Domain.Entities;

namespace SmartHouseManagment.AppCore.Extensions.Mapper;

public static class UserMappingExtension
{
    public static User ToEntity(this RegisterUserModel registerUserModel)
        => new()
        {
            Email = registerUserModel.Email,
            UserName = registerUserModel.Name,
            BirthDate = registerUserModel.BirthDate
        };
}