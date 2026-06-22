using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SmartHouseManagment.AppCore.Models;
using SmartHouseManagment.AppCore.Services.Interfaces;
using SmartHouseManagment.Domain.Entities;
using SmartHouseManagment.Domain.Spec.User;

namespace SmartHouseManagment.AppCore.UseCases;

public static class LoginUser
{
    public class Command : IRequest<ResultModel<string>>
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
    
    internal class Validator : AbstractValidator<Command>
    {
        public Validator(
            UserManager<User> userManager)
        {
            User? user = null;
            
            RuleLevelCascadeMode = CascadeMode.Stop;
            
            RuleFor(x => x.Email)
                .EmailAddress()
                .MustAsync(async (x, ct) =>
                {
                    user = await userManager.FindByEmailAsync(x);
                    return user is not null;
                })
                .WithMessage("The user does not exist.");
            
            RuleFor(x => x.Password)
                .MustAsync(async (x, ct) =>
                {
                    if (user is null) 
                        return false;

                    return await userManager.CheckPasswordAsync(user, x);
                })
                .WithMessage("Incorrect password.");
        }
    }
    
    internal class Handler(
        IAuthService authService) : IRequestHandler<Command, ResultModel<string>>
    {
        public async Task<ResultModel<string>> Handle(Command request, CancellationToken cancellationToken)
            => await authService.LoginUserAsync(request.Email, request.Password);
    }
}