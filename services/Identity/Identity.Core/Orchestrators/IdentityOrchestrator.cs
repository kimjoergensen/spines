namespace Identity.Core.Orchestrators;

using System.Threading.Tasks;

using Identity.Core.Exceptions;
using Identity.Core.Models;
using Identity.Core.Models.Commands;
using Identity.Core.Models.Queries;
using Identity.Core.Models.Requests;
using Identity.Core.Orchestrators.Interfaces;

using Microsoft.Extensions.Logging;

using Spines.Shared.Mediators;

public class IdentityOrchestrator : IIdentityOrchestrator
{
    private readonly ILogger _logger;
    private readonly IMediator _mediator;

    public IdentityOrchestrator(ILogger<IdentityOrchestrator> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    /// <summary>
    /// Register a new user.
    /// </summary>
    /// <param name="request">Login credentials for new user.</param>
    /// <returns>Newly registered <see cref="ApplicationUser"/>.</returns>
    /// <exception cref="RegisterUserException"/>
    public async ValueTask<Token> RegisterUserAsync(RegisterUserRequest request)
    {
        var registerUserCommand = new RegisterUserCommand { Email = request.Email, Password = request.Password };
        var user = await _mediator.InvokeAsync<ApplicationUser>(registerUserCommand);
        return await AuthenticateUserAsync(user!, request.Password);
    }

    /// <summary>
    /// Authenticate a user by the login credentials.
    /// </summary>
    /// <param name="request">Login credentials of the user.</param>
    /// <returns>Token object containing Bearer JWT and expiration in minutes.</returns>
    /// <exception cref="AuthenticateUserException"/>
    public async ValueTask<Token> AuthenticateUserAsync(AuthenticateUserRequest request)
    {
        var getUserQuery = new GetUserQuery { Username = request.Email };
        var user = await _mediator.InvokeAsync<ApplicationUser>(getUserQuery);

        if (user is null)
        {
            _logger.LogInformation("User '{user}' is not registered.", request.Email);
            throw new AuthenticateUserException(request.Email);
        }

        return await AuthenticateUserAsync(user, request.Password);
    }

    private async ValueTask<Token> AuthenticateUserAsync(ApplicationUser user, string password)
    {
        var authenticateUserQuery = new AuthenticateUserQuery { User = user, Password = password };
        var token = await _mediator.InvokeAsync<Token>(authenticateUserQuery);
        return token!;
    }

    /// <summary>
    /// Sign out a user identified by the email.
    /// </summary>
    /// <param name="request">Credentials of user to sign out.</param>
    public async ValueTask SignOutUserAsync(SignOutUserRequest request)
    {
        var logoutUserCommand = new SignOutUserCommand { Email = request.Email };
        await _mediator.InvokeAsync(logoutUserCommand);
    }
}
