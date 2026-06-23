using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartHouseManagment.AppCore.Configurations;
using SmartHouseManagment.AppCore.UseCases;

namespace SmartHouseManagment.Api.v1;

[ApiController]
[Route("api/v1/User")]
public class UserController(
    IMediator mediator) : BaseController(mediator)
{
    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser(
        [FromBody] RegisterUser.Command request,
        CancellationToken cancellationToken)
    {
        var result = await Mediator.SendAsync(request, cancellationToken);
        
        return HandleResult(result);
    }
    
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> LoginUser(
        [FromBody] LoginUser.Command request,
        CancellationToken cancellationToken)
    {
        var result = await Mediator.SendAsync(request, cancellationToken);
        
        return HandleResult(result);
    }
}