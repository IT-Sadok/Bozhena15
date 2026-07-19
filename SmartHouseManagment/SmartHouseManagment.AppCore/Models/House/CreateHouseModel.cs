namespace SmartHouseManagment.AppCore.Models.House;

public class CreateHouseModel
{
    public required string Name { get; set; }
    public required AddressModel Address { get; set; }
}
