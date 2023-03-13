namespace Identity.Core.Services;
using System.Threading.Tasks;

using Identity.Core.Models;
using Identity.Core.Models.Queries;
using Identity.Core.Models.Requests;
using Identity.Core.Services.Interfaces;

using Microsoft.Extensions.Logging;

using Spines.Shared.Mediators;

public class IdentityService : IIdentityService
{
    private readonly ILogger _logger;
    private readonly IMediator _mediator;

    public IdentityService(ILogger<IdentityService> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    /// <summary>
    /// Log in a user for the specified login credentials.
    /// </summary>
    /// <param name="query">Login credentials of the user.</param>
    /// <returns><see cref="ApplicationUser"/></returns>
    /// <exception cref="AuthenticateUserException"/>
    public async Task<Token> AuthenticateUserAsync(AuthenticateUserQuery query)
    {
        var user = await _mediator.InvokeAsync<ApplicationUser>(new GetUserRequest { Username = query.Username });
        return await _mediator.InvokeAsync<Token>(new AuthenticateUserRequest { User = user, Password = query.Password });
    }
}
