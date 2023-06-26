namespace Identity.API.Services;

using System.Threading.Tasks;

using Grpc.Core;

using Identity.API.Services.Interfaces;
using Identity.Core.Exceptions;
using Identity.Core.Mediators.Interfaces;
using Identity.Core.Models.Queries;

using Microsoft.AspNetCore.Authorization;

[Authorize]
public class IdentityService : IIdentityService
{
    private readonly ILogger _logger;
    private readonly IIdentityMediator _service;

    public IdentityService(ILogger<IdentityService> logger, IIdentityMediator service)
    {
        _logger = logger;
        _service = service;
    }

    [AllowAnonymous]
    public async ValueTask<AuthenticateUserResponse> Authenticate(AuthenticateUserRequest request)
    {
        try
        {
            var token = await _service.AuthenticateUserAsync(new AuthenticateUserQuery { Username = request.Username, Password = request.Password });
            return new AuthenticateUserResponse { AccessToken = token.AccessToken, Expires = TimeSpan.FromMinutes(30) };
        }
        catch (AuthenticateUserException ex)
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, ex.Message));
        }
    }
}