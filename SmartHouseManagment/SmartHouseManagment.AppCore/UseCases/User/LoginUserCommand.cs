using FluentValidation;
using Microsoft.AspNetCore.Identity;
using SmartHouseManagment.AppCore.Configurations;
using SmartHouseManagment.AppCore.Models;
using SmartHouseManagment.AppCore.Models.User;
using SmartHouseManagment.AppCore.Services.Interfaces;
using UserEntity =  SmartHouseManagment.Domain.Entities.User;

namespace SmartHouseManagment.AppCore.UseCases.User;

public static class LoginUserCommand
{
    public class Command : IRequest<ResultModel<LoginUserResponse>>
    {
        public required LoginUserModel User { get; set; }
    }
    
    internal class Validator : AbstractValidator<Command>
    {
        public Validator(
            UserManager<UserEntity> userManager)
        {
            UserEntity? user = null;
            
            RuleLevelCascadeMode = CascadeMode.Stop;
            
            RuleFor(x => x.User.Email)
                .EmailAddress()
                .MustAsync(async (x, ct) =>
                {
                    user = await userManager.FindByEmailAsync(x);
                    return user is not null;
                })
                .WithMessage("The user does not exist.");
            
            RuleFor(x => x.User.Password)
                .MustAsync(async (x, ct) => await userManager.CheckPasswordAsync(user!, x))
                .When(_ => user is not null)
                .WithMessage("Incorrect password.");
        }
    }
    
    public class Handler(
        IAuthService authService) : IRequestHandler<Command, ResultModel<LoginUserResponse>>
    {
        public async Task<ResultModel<LoginUserResponse>> HandleAsync(Command request, CancellationToken cancellationToken)
            => await authService.LoginUserAsync(request.User, cancellationToken);
    }
}