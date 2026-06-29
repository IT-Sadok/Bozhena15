namespace SmartHouseManagment.AppCore.Models.User;

public record LoginUserResponse
{
    public required string Token { get; set; }
}
