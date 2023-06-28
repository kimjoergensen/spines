namespace Identity.Core.Mediators;

using Identity.Core.Exceptions;
using Identity.Core.Models;
using Identity.Core.Models.Commands;
using Identity.Core.Models.Requests;
using Identity.Core.Services.Interfaces;

using Microsoft.Extensions.Logging;

using Spines.Shared.Mediators;

public class UserMediator : IUserMediator
{
    private readonly ILogger<UserMediator> _logger;
    private readonly IMediator _mediator;

    public UserMediator(ILogger<UserMediator> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    /// <summary>
    /// Register a new user.
    /// </summary>
    /// <param name="command">Mediator command for registering a new user.</param>
    /// <returns>Registered <see cref="ApplicationUser"/>.</returns>
    /// <exception cref="UserRegistrationException"/>
    public async Task<ApplicationUser?> RegisterUserAsync(RegisterUserCommand command) =>
        await _mediator.InvokeAsync<ApplicationUser>(new RegisterUserRequest { Email = command.Email, Password = command.Password });
}