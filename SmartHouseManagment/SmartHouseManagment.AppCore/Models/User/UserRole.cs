using System.ComponentModel;

namespace SmartHouseManagment.AppCore.Models.User;

public enum UserRole
{
    [Description("User")]
    User = 0,
    
    [Description("Admin")]
    Admin
}