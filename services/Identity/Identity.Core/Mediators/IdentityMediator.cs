namespace Identity.Core.Mediators;

using System.Threading.Tasks;

using Identity.Core.Exceptions;
using Identity.Core.Mediators.Interfaces;
using Identity.Core.Models;
using Identity.Core.Models.Queries;
using Identity.Core.Models.Requests;

using Microsoft.Extensions.Logging;

using Spines.Shared.Mediators;

public class IdentityMediator : IIdentityMediator
{
    private readonly ILogger _logger;
    private readonly IMediator _mediator;

    public IdentityMediator(ILogger<IdentityMediator> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    /// <summary>
    /// Authenticate a user by the login credentials.
    /// </summary>
    /// <param name="query">Login credentials of the user.</param>
    /// <returns>JWT Bearer token.</returns>
    /// <exception cref="AuthenticateUserException"/>
    public async Task<Token> AuthenticateUserAsync(AuthenticateUserQuery query)
    {
        var user = await _mediator.InvokeAsync<ApplicationUser>(new GetUserRequest { Username = query.Username });

        if (user is null)
        {
            _logger.LogInformation("User '{user}' is not registered.", query.Username);
            throw new AuthenticateUserException(query.Username);
        }

        var token = await _mediator.InvokeAsync<Token>(new AuthenticateUserRequest { User = user, Password = query.Password });
        return token!;
    }
}
