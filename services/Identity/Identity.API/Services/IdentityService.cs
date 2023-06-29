namespace Identity.API.Services;

using System.Threading.Tasks;

using Grpc.Core;

using Identity.API.Services.Interfaces;
using Identity.Core.Exceptions;
using Identity.Core.Models.Requests;
using Identity.Core.Models.Responses;
using Identity.Core.Orchestrators.Interfaces;

using Microsoft.AspNetCore.Authorization;

[Authorize]
public class IdentityService : IIdentityService
{
    private readonly ILogger _logger;
    private readonly IIdentityOrchestrator _identityOrchestrator;

    public IdentityService(ILogger<IdentityService> logger, IIdentityOrchestrator orchestrator)
    {
        _logger = logger;
        _identityOrchestrator = orchestrator;
    }

    [AllowAnonymous]
    public async ValueTask<AuthenticateUserResponse> Authenticate(AuthenticateUserRequest request)
    {
        try
        {
            var token = await _identityOrchestrator.AuthenticateUserAsync(request);
            return new AuthenticateUserResponse { AccessToken = token.AccessToken, Expires = TimeSpan.FromMinutes(30) };
        }
        catch (AuthenticateUserException ex)
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, ex.Message));
        }
    }
}