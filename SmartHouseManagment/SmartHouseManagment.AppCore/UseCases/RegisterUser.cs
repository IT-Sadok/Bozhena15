using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using SmartHouseManagment.AppCore.Dtos;
using SmartHouseManagment.AppCore.Extensions.Mapper;
using SmartHouseManagment.AppCore.Models;
using SmartHouseManagment.AppCore.Services.Interfaces;
using SmartHouseManagment.Domain.Entities;
using SmartHouseManagment.Domain.Spec.User;

namespace SmartHouseManagment.AppCore.UseCases;

public static class RegisterUser
{
    public class Command : IRequest<ResultModel<User>>
    {
        public required RegisterUserDto User { get; set; }
    }

    internal class Validator : AbstractValidator<Command>
    {
        public Validator(
            IRepository repository)
        {
            RuleFor(x => x.User.Email)
                .NotEmpty()
                .MaximumLength(50)
                .EmailAddress()
                .MustAsync(async (x, ct) =>
                {
                    var existingUser = await repository.Entity<User>().ExistsAsync(new UserByEmailSpec(x), ct);
                    return !existingUser;
                })
                .WithMessage("Email already exists");

            RuleFor(x => x.User.Name)
                .NotEmpty()
                .MaximumLength(50);
        }
    }
    
    internal class Handler(
        IRepository repository,
        IPasswordHasher<User> passwordHasher) : IRequestHandler<Command, ResultModel<User>>
    {
        public async Task<ResultModel<User>> Handle(Command request, CancellationToken cancellationToken)
        {
            var user = request.User.ToEntity(passwordHasher);
            
            await repository.Entity<User>().AddAsync(user, cancellationToken);
            await repository.Entity<User>().SaveChangesAsync(cancellationToken);
            
            return new ResultModel<User>(user);
        }
    }
}