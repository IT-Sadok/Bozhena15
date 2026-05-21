namespace SmartHouseManagment.Domain.Spec.User;

public class UserByEmailSpec : SpecificationBase<Entities.User>
{
    public UserByEmailSpec(string email)
    {
        ApplyFilter(x => x.Email == email);
    }
}