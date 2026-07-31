namespace SmartHouseManagment.AppCore.Models.House;

public record AddressModel
{
    public required string Address1 { get; set; }
    public string Address2 { get; set; } = string.Empty;
    public required string City { get; set; }
    public string State { get; set; } = string.Empty;
    public required string ZipCode { get; set; }
    public required string Country { get; set; }
}
