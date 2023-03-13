namespace Identity.API.Grpc;

using System.Threading.Tasks;

using Google.Protobuf.WellKnownTypes;

using Identity.API.Protos;
using Identity.Core.Models.Commands;
using Identity.Core.Services.Interfaces;

public class UsersService : Users.UsersBase
{
    private readonly ILogger _logger;
    private readonly IUserService _service;

    public UsersService(ILogger<UsersService> logger, IUserService service)
    {
        _logger = logger;
        _service = service;
    }

    public override async Task<Empty> Register(RegisterUserRequest request, global::Grpc.Core.ServerCallContext context)
    {
        await _service.RegisterUserAsync(new RegisterUserCommand { Email = request.Email, Password = request.Password });
        return new Empty();
    }
}
