namespace SmartHouseManagment.Domain.Entities;

public abstract class BaseEntity
{
    public Guid Id { get; set; } 
    public DateTime DateCreated { get; set; } = DateTime.UtcNow;
}