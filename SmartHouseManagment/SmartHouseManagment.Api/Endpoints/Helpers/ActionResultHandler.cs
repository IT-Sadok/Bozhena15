using SmartHouseManagment.AppCore.Models;

namespace SmartHouseManagment.Api.Endpoints.Helpers;

public static class ActionResultHandler
{
    public static IResult Handle(ResultModel result)
    {
        if (result.IsSuccess)
            return Results.Ok(result);

        if (result.Error == null)
            return Results.InternalServerError("An unknown error occurred.");

        return result.Error.Type switch
        {
            ErrorTypes.NotFound => Results.NotFound(result.Error.Description),
            ErrorTypes.BadRequest => Results.BadRequest(result.Error.Description),
            ErrorTypes.Unauthorized => Results.Unauthorized(),
            ErrorTypes.Conflict => Results.Conflict(result.Error.Description),
            _ => Results.InternalServerError(result.Error.Description)
        };
    }
}
