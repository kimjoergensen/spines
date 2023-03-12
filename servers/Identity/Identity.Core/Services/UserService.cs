namespace Identity.Core.Services;
using Identity.Core.Models.Commands;
using Identity.Core.Services.Interfaces;

using Spines.Shared.Mediators;

public class UserService : IUserService
{
    private readonly IMediator _mediator;

    public UserService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public Task RegisterUserAsync(RegisterUserCommand command) =>
        _mediator.InvokeAsync(command);
}
