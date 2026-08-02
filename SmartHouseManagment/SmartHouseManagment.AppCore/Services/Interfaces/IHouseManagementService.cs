using SmartHouseManagment.AppCore.Models;
using SmartHouseManagment.AppCore.Models.House;

namespace SmartHouseManagment.AppCore.Services.Interfaces;

public interface IHouseManagementService
{
    Task<ResultModel<CreateHouseResponse>> CreateHouseAsync(CreateHouseModel createHouseModel, CancellationToken cancellationToken);
}
