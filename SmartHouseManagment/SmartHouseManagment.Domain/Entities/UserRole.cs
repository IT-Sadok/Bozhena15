using System.ComponentModel;

namespace SmartHouseManagment.Domain.Entities;

public enum UserRole
{
    [Description("Admin")]
    Admin,
    
    [Description("User")]
    User
}