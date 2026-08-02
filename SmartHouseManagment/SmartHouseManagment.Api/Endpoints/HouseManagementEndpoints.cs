using Microsoft.AspNetCore.Mvc;
using SmartHouseManagment.Api.Endpoints.Helpers;
using SmartHouseManagment.AppCore.Configurations;
using SmartHouseManagment.AppCore.Extensions;
using SmartHouseManagment.AppCore.Models.House;
using SmartHouseManagment.AppCore.Models.User;
using SmartHouseManagment.AppCore.UseCases.User;

namespace SmartHouseManagment.Api.Endpoints;

public static class HouseManagementEndpoints
{
    public static RouteGroupBuilder MapHouseManagementEndpoints(this RouteGroupBuilder group)
    {
        group.MapPost("/", async (
            [FromBody] CreateHouseCommand.Command request,
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
