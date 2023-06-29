namespace Identity.Core.Orchestrators;

using Identity.Core.Exceptions;
using Identity.Core.Models;
using Identity.Core.Models.Commands;
using Identity.Core.Models.Requests;
using Identity.Core.Orchestrators.Interfaces;

using Microsoft.Extensions.Logging;

using Spines.Shared.Mediators;

public class UserOrchestrator : IUserOrchestrator
{
    private readonly ILogger<UserOrchestrator> _logger;
    private readonly IMediator _mediator;

    public UserOrchestrator(ILogger<UserOrchestrator> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    /// <summary>
    /// Register a new user.
    /// </summary>
    /// <param name="request">Login credentials for new user.</param>
    /// <returns>Newly registered <see cref="ApplicationUser"/>.</returns>
    /// <exception cref="UserRegistrationException"/>
    public async Task<ApplicationUser?> RegisterUserAsync(RegisterUserRequest request)
    {
        var registerUserCommand = new RegisterUserCommand { Email = request.Email, Password = request.Password };
        return await _mediator.InvokeAsync<ApplicationUser>(registerUserCommand);
    }
}