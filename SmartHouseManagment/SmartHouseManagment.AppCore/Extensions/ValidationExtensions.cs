using System.Text.RegularExpressions;
using FluentValidation;

namespace SmartHouseManagment.AppCore.Extensions;

public static class ValidationExtensions
{
    private static readonly Regex EmailAddressRegex = new(
        """^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$""",
        RegexOptions.Compiled);
    
    public static IRuleBuilderOptions<T, string> MustHaveValidEmail<T>(this IRuleBuilder<T, string> ruleBuilder)
        => ruleBuilder
            .Must(x => EmailAddressRegex.IsMatch(x))
            .WithMessage("Email is not valid");
}