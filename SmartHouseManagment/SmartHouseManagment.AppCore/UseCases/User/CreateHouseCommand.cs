using FluentValidation;
using SmartHouseManagment.AppCore.Configurations;
using SmartHouseManagment.AppCore.Models;
using SmartHouseManagment.AppCore.Models.House;
using SmartHouseManagment.AppCore.Services.Interfaces;
using SmartHouseManagment.AppCore.Validators;

namespace SmartHouseManagment.AppCore.UseCases.User;

public static class CreateHouseCommand
{
    public class Command : IRequest<ResultModel<CreateHouseResponse>>
    {
        public required CreateHouseModel House { get; set; }
    }

    internal class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.House)
                .MustHaveValidHouseInfo();
        }
    }

    public class Handler(
        IHouseManagementService houseService) : IRequestHandler<Command, ResultModel<CreateHouseResponse>>
    {
        public async Task<ResultModel<CreateHouseResponse>> HandleAsync(Command request, CancellationToken cancellationToken)
            => await houseService.CreateHouseAsync(request.House, cancellationToken);
    }
}
