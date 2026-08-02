namespace SmartHouseManagment.AppCore.Models.User;

public record RegisterUserModel(string Name, string Email, string Password, DateOnly BirthDate, UserRole Role);