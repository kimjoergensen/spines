namespace Identity.API.Services;

using System.Threading.Tasks;

using Grpc.Core;

using Identity.API.Services.Interfaces;
using Identity.Core.Exceptions;
using Identity.Core.Models.Queries;
using Identity.Core.Services.Interfaces;

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

    //[AllowAnonymous]
    //public override async Task<AuthenticationResponse> Authenticate(AuthenticationRequest request, ServerCallContext context)
    //{
    //    try
    //    {
    //        var token = await _service.AuthenticateUserAsync(new AuthenticateUserQuery { Username = request.Username, Password = request.Password });
    //        var expires = DateTimeOffset.Now.AddMinutes(token.ExpiresIn);
    //        return new AuthenticationResponse { AccessToken = token.AccessToken, Expires = Timestamp.FromDateTimeOffset(expires) };
    //    }
    //    catch (AuthenticateUserException ex)
    //    {
    //        throw new RpcException(new Status(StatusCode.InvalidArgument, ex.Message));
    //    }
    //}

    [AllowAnonymous]
    public async ValueTask<AuthenticateUserResponse> Authenticate(AuthenticateUserRequest request)
    {
        try
        {
            var token = await _service.AuthenticateUserAsync(new AuthenticateUserQuery { Username = request.Username, Password = request.Password });
            var expires = DateTimeOffset.Now.AddMinutes(token.ExpiresIn);
            return new AuthenticateUserResponse { AccessToken = token.AccessToken, Expires = expires };
        }
        catch (AuthenticateUserException ex)
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, ex.Message));
        }
    }
}