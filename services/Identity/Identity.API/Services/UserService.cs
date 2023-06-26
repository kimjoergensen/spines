namespace Identity.API.Services;

using System.Threading.Tasks;

using Grpc.Core;

using Identity.API.Services.Interfaces;
using Identity.Core.Exceptions;
using Identity.Core.Models.Commands;
using Identity.Core.Services.Interfaces;

public class UserService : IUserService
{
    private readonly ILogger _logger;
    private readonly IUserMediator _mediator;

    public UserService(ILogger<UserService> logger, IUserMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    public async ValueTask RegisterUser(RegisterUserRequest request)
    {
        try
        {
            await _mediator.RegisterUserAsync(new RegisterUserCommand { Email = request.Email, Password = request.Password });
        }
        catch (UserRegistrationException ex)
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, ex.Message));
        }
    }
}
