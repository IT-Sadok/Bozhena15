using Microsoft.AspNetCore.Identity;

namespace SmartHouseManagment.Domain.Entities;

public class User : IdentityUser, IEntity
{
    public required DateOnly BirthDate { get; set; }
}