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

    /// <summary>
    /// Creates a new user.
    /// </summary>
    /// <param name="request">Login credentials for the new user.</param>
    /// <returns>User authentication response with JWT Bearer token and expiration time.</returns>
    /// <exception cref="RpcException" />
    public async ValueTask<AuthenticateUserResponse> Register(RegisterUserRequest request)
    {
        try
        {
            var token = await _identityOrchestrator.RegisterUserAsync(request);
            return new AuthenticateUserResponse { AccessToken = token.AccessToken, Expires = TimeSpan.FromMinutes(token.ExpiresIn) };
        }
        catch (RegisterUserException ex)
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, ex.Message));
        }
    }

    /// <summary>
    /// Authenticates a user.
    /// </summary>
    /// <param name="request">Login credentials for the user to authenticate.</param>
    /// <returns>User authentication response with JWT Bearer token and expiration time.</returns>
    /// <exception cref="RpcException" />
    [AllowAnonymous]
    public async ValueTask<AuthenticateUserResponse> SignIn(AuthenticateUserRequest request)
    {
        try
        {
            var token = await _identityOrchestrator.AuthenticateUserAsync(request);
            return new AuthenticateUserResponse { AccessToken = token.AccessToken, Expires = TimeSpan.FromMinutes(token.ExpiresIn) };
        }
        catch (AuthenticateUserException ex)
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, ex.Message));
        }
    }

    /// <summary>
    /// Sign out a user.
    /// </summary>
    /// <param name="request">Credentials of user to sign out.</param>
    /// <exception cref="RpcException" />
    public async ValueTask SignOut(SignOutUserRequest request)
    {
        await _identityOrchestrator.SignOutUserAsync(request);
    }
}