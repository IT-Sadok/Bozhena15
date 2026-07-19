using SmartHouseManagment.AppCore.Models.House;
using SmartHouseManagment.Domain.Entities;

namespace SmartHouseManagment.AppCore.Extensions.Mapper;

public static class HouseMappingExtensions
{
    public static House ToEntity(this CreateHouseModel createHouseModel)
        => new()
        {
            Name = createHouseModel.Name,
            Address = new Address
            {
                Street = createHouseModel.Address.Street,
                Street2 = createHouseModel.Address.Street2,
                City = createHouseModel.Address.City,
                State = createHouseModel.Address.State,
                ZipCode = createHouseModel.Address.ZipCode,
                Country = createHouseModel.Address.Country,
            }
        };

    public static CreateHouseResponse ToCreateHouseResponse(this House entity)
        => new()
        {
            Id = entity.Id,
            Name = entity.Name,
            Address = new AddressModel
            {
                Street = entity.Address.Street,
                Street2 = entity.Address.Street2,
                City = entity.Address.City,
                State = entity.Address.State,
                ZipCode = entity.Address.ZipCode,
                Country = entity.Address.Country,
            }
        };
}
