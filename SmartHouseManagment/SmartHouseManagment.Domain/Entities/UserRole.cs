using System.ComponentModel;

namespace SmartHouseManagment.Domain.Entities;

public enum UserRole
{
    [Description("User")]
    User,
    
    [Description("Admin")]
    Admin,
}