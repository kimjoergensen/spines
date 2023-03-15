namespace Identity.API.Grpc;

using System.Threading.Tasks;

using global::Grpc.Core;

using Identity.API.Protos;
using Identity.Core.Models.Queries;
using Identity.Core.Services.Interfaces;

using Microsoft.AspNetCore.Authorization;

[Authorize]
public class IdentityService : Identity.IdentityBase
{
    private readonly ILogger _logger;
    private readonly IIdentityMediator _service;

    public IdentityService(ILogger<IdentityService> logger, IIdentityMediator service)
    {
        _logger = logger;
        _service = service;
    }

    [AllowAnonymous]
    public override async Task<AuthenticationResponse> Authenticate(AuthenticationRequest request, ServerCallContext context)
    {
        var token = await _service.AuthenticateUserAsync(new AuthenticateUserQuery { Username = request.Username, Password = request.Password });
        return new AuthenticationResponse { AccessToken = token.AccessToken, ExpiresIn = token.ExpiresIn };
    }
}