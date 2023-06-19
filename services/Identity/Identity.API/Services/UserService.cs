namespace Identity.API.Services;

using System.Threading.Tasks;

using Google.Protobuf.WellKnownTypes;

using Grpc.Core;

using Identity.API.Protos;
using Identity.Core.Exceptions;
using Identity.Core.Models.Commands;
using Identity.Core.Services.Interfaces;

using Spines.Shared.Extensions;

public class UserService : User.UserBase
{
    private readonly ILogger _logger;
    private readonly IUserMediator _mediator;

    public UserService(ILogger<UserService> logger, IUserMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    public override async Task<Empty> Register(RegisterUserRequest request, ServerCallContext context)
    {
        try
        {
            await _mediator.RegisterUserAsync(new RegisterUserCommand { Email = request.Email, Password = request.Password });
            return new Empty();
        }
        catch (UserRegistrationException ex)
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, ex.Message), new Metadata().ConvertIdentityErrors(ex.Errors));
        }
    }
}
