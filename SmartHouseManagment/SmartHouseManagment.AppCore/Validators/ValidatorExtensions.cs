using FluentValidation;
using SmartHouseManagment.AppCore.Models.House;

namespace SmartHouseManagment.AppCore.Validators;

public static class ValidatorExtensions
{
    public static IRuleBuilderOptions<T, CreateHouseModel> MustHaveValidHouseInfo<T>(
        this IRuleBuilder<T, CreateHouseModel> ruleBuilder)
        => ruleBuilder.SetValidator(x => new HouseValidator());
}
