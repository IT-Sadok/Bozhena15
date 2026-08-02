using FluentValidation;
using SmartHouseManagment.AppCore.Models.House;

namespace SmartHouseManagment.AppCore.Validators;

public class HouseValidator : AbstractValidator<CreateHouseModel>
{
    public HouseValidator()
    {
        RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100)
                .WithMessage(Constants.ValidationErrors.RequiredField("House name"));

        RuleFor(x => x.Address)
            .ChildRules(house =>
            {
                house.RuleFor(x => x.Address1)
                    .NotEmpty()
                    .MaximumLength(100)
                    .WithMessage(Constants.ValidationErrors.RequiredField("Street"));

                house.RuleFor(x => x.Address2)
                    .MaximumLength(100);

                house.RuleFor(x => x.City)
                    .NotEmpty()
                    .MaximumLength(30)
                    .WithMessage(Constants.ValidationErrors.RequiredField("City"));

                house.RuleFor(x => x.State)
                    .MaximumLength(30)
                    .WithMessage(Constants.ValidationErrors.RequiredField("State"));

                house.RuleFor(x => x.ZipCode)
                    .NotEmpty()
                    .MaximumLength(10)
                    .WithMessage(Constants.ValidationErrors.RequiredField("Zip code"));

                house.RuleFor(x => x.Country)
                    .NotEmpty()
                    .MaximumLength(50)
                    .WithMessage(Constants.ValidationErrors.RequiredField("Country"));
            });
    }
}
