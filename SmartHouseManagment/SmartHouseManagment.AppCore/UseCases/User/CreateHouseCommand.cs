using FluentValidation;
using SmartHouseManagment.AppCore.Configurations;
using SmartHouseManagment.AppCore.Models;
using SmartHouseManagment.AppCore.Models.House;
using SmartHouseManagment.AppCore.Services.Interfaces;

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
            RuleFor(x => x.House.Name)
                .NotEmpty()
                .MaximumLength(100)
                .WithMessage("House name is required.");

            RuleFor(x => x.House.Address)
                .ChildRules(house =>
                {
                    house.RuleFor(x => x.Street)
                        .NotEmpty()
                        .MaximumLength(100)
                        .WithMessage("Street is required.");

                    house.RuleFor(x => x.Street2)
                        .MaximumLength(100);

                    house.RuleFor(x => x.City)
                        .NotEmpty()
                        .MaximumLength(30)
                        .WithMessage("City is required.");

                    house.RuleFor(x => x.State)
                        .MaximumLength(30)
                        .WithMessage("State is required.");

                    house.RuleFor(x => x.ZipCode)
                        .NotEmpty()
                        .MaximumLength(10)
                        .WithMessage("Zip code is required.");

                    house.RuleFor(x => x.Country)
                        .NotEmpty()
                        .MaximumLength(50)
                        .WithMessage("Country is required.");
                });

        }
    }

    public class Handler(
        IHouseManagementService houseService) : IRequestHandler<Command, ResultModel<CreateHouseResponse>>
    {
        public async Task<ResultModel<CreateHouseResponse>> HandleAsync(Command request, CancellationToken cancellationToken)
            => await houseService.CreateHouseAsync(request.House, cancellationToken);
    }
}
