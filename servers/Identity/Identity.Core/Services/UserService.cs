namespace Identity.Core.Services;

using System.Security.Claims;

using Identity.Core.Exceptions;
using Identity.Core.Models;
using Identity.Core.Models.Commands;
using Identity.Core.Models.Requests;
using Identity.Core.Services.Interfaces;

using Microsoft.Extensions.Logging;

using Spines.Shared.Mediators;

public class UserService : IUserService
{
    private readonly ILogger<UserService> _logger;
    private readonly IMediator _mediator;

    public UserService(ILogger<UserService> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    /// <summary>
    /// Register a new user.
    /// </summary>
    /// <param name="command">Mediator command for registering a new user.</param>
    /// <returns>Registered user's <see cref="ClaimsPrincipal"/>.</returns>
    /// <exception cref="UserRegistrationException"/>
    public async Task RegisterUserAsync(RegisterUserCommand command) =>
        await _mediator.InvokeAsync<ApplicationUser>(new RegisterUserRequest { Email = command.Email, Password = command.Password });
}