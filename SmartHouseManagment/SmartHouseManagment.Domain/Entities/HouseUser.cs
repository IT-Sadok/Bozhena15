namespace SmartHouseManagment.Domain.Entities;

public class HouseUser : IEntity
{
    public required Guid HouseId { get; set; }
    public House House { get; set; } = null!;

    public required Guid UserId { get; set; }
    public User User { get; set; } = null!;

    public required Guid AdminId { get; set; }
}
