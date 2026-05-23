using Microsoft.AspNetCore.Mvc;
using SmartHouseManagment.AppCore.Dtos;
using SmartHouseManagment.AppCore.UseCases;

namespace SmartHouseManagment.Api.v1;

[ApiController]
[Route("api/v1/User")]
public class UserController: BaseController
{
    [HttpPost("register")]
    public async Task<ActionResult> RegisterUser(
        [FromBody] RegisterUser.Command request,
        CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(request, cancellationToken);
        
        return result.IsError
            ? BadRequest(result.Errors)
            : Ok(result.Data);
    }
    
    [HttpPost("login")]
    public async Task<ActionResult> LoginUser(
        [FromBody] LoginUser.Command request,
        CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(request, cancellationToken);
        
        return result.IsError
            ? BadRequest(result.Errors)
            : Ok(result.Data);
    }
}