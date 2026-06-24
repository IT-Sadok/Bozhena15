using FluentValidation;
using Microsoft.AspNetCore.Identity;
using SmartHouseManagment.AppCore.Configurations;
using SmartHouseManagment.AppCore.Models;
using SmartHouseManagment.AppCore.Models.User;
using SmartHouseManagment.AppCore.Services.Interfaces;
using SmartHouseManagment.Domain.Entities;

namespace SmartHouseManagment.AppCore.UseCases;

public static class RegisterUser
{
    public class Command : IRequest<ResultModel<string>>
    {
        public required RegisterUserModel User { get; set; }
    }

    internal class Validator : AbstractValidator<Command>
    {
        public Validator(
            UserManager<User> userManager)
        {
            RuleLevelCascadeMode = CascadeMode.Stop;
            
            RuleFor(x => x.User.Email)
                .NotEmpty()
                .MaximumLength(50)
                .EmailAddress()
                .MustAsync(async (x, ct) =>
                {
                    var user = await userManager.FindByEmailAsync(x);
                    return user is null;
                })
                .WithMessage("Email already exists");

            RuleFor(x => x.User.Password)
                .CustomAsync(async (password, context, cancellationToken) =>
                {
                    foreach (var validator in userManager.PasswordValidators)
                    {
                        var result = await validator.ValidateAsync(
                            userManager,
                            null!,
                            password);

                        if (!result.Succeeded)
                        {
                            foreach (var error in result.Errors)
                            {
                                context.AddFailure(error.Description);
                            }
                        }
                    }
                });
            
            RuleFor(x => x.User.Name)
                .NotEmpty()
                .MaximumLength(50);
        }
    }
    
    internal class Handler(
        IAuthService authService) : IRequestHandler<Command, ResultModel<string>>
    {
        public async Task<ResultModel<string>> HandleAsync(Command request, CancellationToken cancellationToken)
            => await authService.RegisterUserAsync(request.User, cancellationToken);
    }
}