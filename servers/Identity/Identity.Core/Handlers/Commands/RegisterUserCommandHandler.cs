namespace Identity.Core.Handlers.Commands;
using System;
using System.Threading.Tasks;

using Identity.Core.Models.Commands;

using Spines.Shared.Mediator;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand>
{
    public Task HandleAsync(RegisterUserCommand request)
    {
        throw new NotImplementedException();
    }
}
