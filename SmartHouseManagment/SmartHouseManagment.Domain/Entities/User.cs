using Microsoft.AspNetCore.Identity;

namespace SmartHouseManagment.Domain.Entities;

public class User : IdentityUser<Guid>, IEntity
{
    public required DateOnly BirthDate { get; set; }
    public IList<HouseUser>? HouseUsers { get; set; }
}