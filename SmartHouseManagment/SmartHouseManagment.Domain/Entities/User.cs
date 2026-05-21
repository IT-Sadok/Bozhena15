namespace SmartHouseManagment.Domain.Entities;

public class User : BaseEntity
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public string PasswordHash { get; set; }
    public required UserRole Role { get; set; }
}