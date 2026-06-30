using Microsoft.AspNetCore.Mvc;
using SmartHouseManagment.Api.Endpoints.Helpers;
using SmartHouseManagment.AppCore.Configurations;
using SmartHouseManagment.AppCore.UseCases.User;

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

        return group;
    }
}