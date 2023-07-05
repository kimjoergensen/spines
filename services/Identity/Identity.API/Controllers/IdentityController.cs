namespace Identity.API.Controllers;

using Identity.Core.Orchestrators.Interfaces;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
[ApiController]
[Route("api/v{v:apiVersion}/[controller]")]
public class IdentityController : ControllerBase
{
    private readonly IIdentityOrchestrator _identityOrchestrator;

    public IdentityController(IIdentityOrchestrator identityOrchestrator)
    {
        _identityOrchestrator = identityOrchestrator;
    }

    //[HttpGet("me")]
    //[AllowAnonymous]
    //public async IActionResult Authenticate()
    //{

    //}
}