namespace SmartHouseManagment.AppCore.Models.House;

public class CreateHouseResponse
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required AddressModel Address { get; set; }
}
