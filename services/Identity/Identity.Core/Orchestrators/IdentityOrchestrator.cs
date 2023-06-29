namespace Identity.Core.Orchestrators;

using System.Threading.Tasks;

using Identity.Core.Exceptions;
using Identity.Core.Models;
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
    /// Authenticate a user by the login credentials.
    /// </summary>
    /// <param name="request">Login credentials of the user.</param>
    /// <returns>Token object containing Bearer JWT and expiration in minutes.</returns>
    /// <exception cref="AuthenticateUserException"/>
    public async Task<Token> AuthenticateUserAsync(AuthenticateUserRequest request)
    {
        var getUserQuery = new GetUserQuery { Username = request.Username };
        var user = await _mediator.InvokeAsync<ApplicationUser>(getUserQuery);

        if (user is null)
        {
            _logger.LogInformation("User '{user}' is not registered.", request.Username);
            throw new AuthenticateUserException(request.Username);
        }

        var authenticateUserQuery = new AuthenticateUserQuery { User = user, Password = request.Password };
        var token = await _mediator.InvokeAsync<Token>(authenticateUserQuery);
        return token!;
    }
}
