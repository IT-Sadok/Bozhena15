using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SmartHouseManagment.AppCore.Dtos;
using SmartHouseManagment.AppCore.Extensions;
using SmartHouseManagment.AppCore.Models;
using SmartHouseManagment.AppCore.Services.Interfaces;
using SmartHouseManagment.Domain.Entities;
using SmartHouseManagment.Domain.Spec.User;

namespace SmartHouseManagment.AppCore.UseCases;

public static class LoginUser
{
    public class Command : IRequest<ResultModel<User?>>
    {
        public required LoginUserDto User { get; set; }
    }
    
    internal class Validator : AbstractValidator<Command>
    {
        public Validator(
            IRepository repository,
            IPasswordHasher<User> passwordHasher)
        {
            User? user = null;
            
            RuleLevelCascadeMode = CascadeMode.Stop;
            
            RuleFor(x => x.User.Email)
                .MustHaveValidEmail()
                .MustAsync(async (x, ct) =>
                {
                    user = await repository.Entity<User>().FindOneAsync(new UserByEmailSpec(x), ct);
                    return user is not null;
                })
                .WithMessage("The user does not exist.");
            
            RuleFor(x => x.User.Password)
                .Must(x =>
                {
                    if (user is null) 
                        return false;
                    
                    return passwordHasher.VerifyHashedPassword(user!, user!.PasswordHash, x) != PasswordVerificationResult.Failed;
                })
                .WithMessage("Incorrect password.");
        }
    }
    
    internal class Handler(
        IRepository repository,
        ILogger<Handler> logger) : IRequestHandler<Command, ResultModel<User?>>
    {
        public async Task<ResultModel<User?>> Handle(Command request, CancellationToken cancellationToken)
        {
            var user = await repository.Entity<User>().FindOneAsync(
                new UserByEmailSpec(request.User.Email), cancellationToken);

            if (user is null)
            {
                logger.LogWarning("{Service}:{Method}: Requested user not found - {email}", 
                    nameof(Handler), 
                    nameof(Handle), 
                    request.User.Email);
                
                return new ResultModel<User?>(Data: null, Errors: ["User not found"], IsError: true);
            }

            return new ResultModel<User?>(user);
        }
    }
}