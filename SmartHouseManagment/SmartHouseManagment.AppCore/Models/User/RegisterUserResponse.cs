namespace SmartHouseManagment.AppCore.Models.User;

public record RegisterUserResponse
{
    public required string Token { get; set; }
}
