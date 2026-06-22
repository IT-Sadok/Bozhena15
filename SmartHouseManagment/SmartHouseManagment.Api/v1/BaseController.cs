using MediatR;
using Microsoft.AspNetCore.Mvc;
using SmartHouseManagment.AppCore.Models;

namespace SmartHouseManagment.Api.v1;

public abstract class BaseController : ControllerBase
{
    private ISender _mediator;
    
    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetService<ISender>();

    protected IActionResult HandleResult(ResultModel result)
    {
        if(result.IsSuccess)
            return Ok(result);

        if (result.Error == null)
            return StatusCode(500, "An unknown error occurred.");

        return result.Error.Type switch
        {
            ErrorTypes.NotFound => NotFound(result.Error.Description),
            ErrorTypes.BadRequest => BadRequest(result.Error.Description),
            ErrorTypes.Unauthorized => Unauthorized(result.Error.Description),
            ErrorTypes.Conflict => Conflict(result.Error.Description),
            _ => StatusCode(500, result.Error.Description)
        };
    }
}