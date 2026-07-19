using SmartHouseManagment.AppCore.Configurations;
using SmartHouseManagment.AppCore.Extensions.Mapper;
using SmartHouseManagment.AppCore.Models;
using SmartHouseManagment.AppCore.Models.House;
using SmartHouseManagment.AppCore.Services.Interfaces;
using SmartHouseManagment.Domain.Entities;

namespace SmartHouseManagment.AppCore.Services;

public class HouseManagementService(
    IUnitOfWork repository): IHouseManagementService
{
    public async Task<ResultModel<CreateHouseResponse>> CreateHouseAsync(CreateHouseModel createHouseModel, CancellationToken cancellationToken)
    {
        var house = createHouseModel.ToEntity();

        await repository.Entity<House>().AddAsync(house, cancellationToken);
        var result = await repository.SaveChangesAsync(cancellationToken);

        if (!result)
            return Constants.Errors.FailedSavingChanges;

        return house.ToCreateHouseResponse();
    }
}
