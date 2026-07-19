namespace SmartHouseManagment.Domain.Entities;

public class Address
{
    public required string Street { get; set; }
    public string Street2 { get; set; } = string.Empty;
    public required string City { get; set; }
    public string State { get; set; } = string.Empty;
    public required string ZipCode { get; set; }
    public required string Country { get; set; }
}
