namespace Identity.API.Grpc;

using System.Threading.Tasks;

using global::Grpc.Core;

using Google.Protobuf.WellKnownTypes;

using Identity.API.Extensions;
using Identity.API.Protos;
using Identity.Core.Exceptions;
using Identity.Core.Models.Commands;
using Identity.Core.Services.Interfaces;

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
