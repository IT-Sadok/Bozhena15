namespace SmartHouseManagment.Domain.Entities;

public class House : Entity
{
    public required string Name { get; set; }
    public required Address Address { get; set; }
    public IList<HouseUser>? HouseUsers { get; set; }
}
