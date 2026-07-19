namespace SmartHouseManagment.Domain.Entities;

public interface IEntity;

public abstract class Entity : IEntity
{
    public Guid Id { get; } = Guid.NewGuid();
    public DateTime DateCreated { get; } = DateTime.UtcNow;
}