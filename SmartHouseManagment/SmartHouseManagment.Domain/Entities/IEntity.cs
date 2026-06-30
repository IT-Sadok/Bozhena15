namespace SmartHouseManagment.Domain.Entities;

public interface IEntity
{
    Guid Id { get => Guid.NewGuid(); } 
    DateTime DateCreated { get => DateTime.UtcNow; }
}