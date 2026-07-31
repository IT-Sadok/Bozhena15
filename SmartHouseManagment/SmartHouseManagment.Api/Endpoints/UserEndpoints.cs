using Microsoft.AspNetCore.Mvc;
using SmartHouseManagment.Api.Endpoints.Helpers;
using SmartHouseManagment.AppCore.Configurations;
using SmartHouseManagment.AppCore.Extensions;
using SmartHouseManagment.AppCore.Models.User;
using SmartHouseManagment.AppCore.UseCases.User;
using System.Security.Claims;

namespace SmartHouseManagment.Api.v1;

public static class UserEndpoints
{
    public static RouteGroupBuilder MapUserEndpoints(this RouteGroupBuilder group)
    {
        group.MapPost("/sign-up", async (
            [FromBody] RegisterUserCommand.Command request,
            IMediator mediator,
            CancellationToken cancellationToken) =>
        {
            if (request.User.Role == UserRole.Admin)
                return Results.BadRequest("Invalid role. Only 'User' role is allowed.");
            
            var result = await mediator.SendAsync(request, cancellationToken);

            return ActionResultHandler.Handle(result);
        })
        .AllowAnonymous();

        group.MapPost("/sign-in", async (
            [FromBody] LoginUserCommand.Command request,
            IMediator mediator,
            CancellationToken cancellationToken) =>
        {
            var result = await mediator.SendAsync(request, cancellationToken);
            return ActionResultHandler.Handle(result);
        })
        .AllowAnonymous();

        group.MapPost("/", async (
            [FromBody] RegisterUserCommand.Command request,
            IMediator mediator,
            CancellationToken cancellationToken) =>
        {
            var result = await mediator.SendAsync(request, cancellationToken);

            return ActionResultHandler.Handle(result);
        })
        .RequireAuthorization(policy => policy.RequireRole(UserRole.Admin.ToEnumDescription()));

        return group;
    }
}