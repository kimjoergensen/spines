namespace Identity.API.Grpc;

using System.Threading.Tasks;

using global::Grpc.Core;
using global::Identity.Core.Models.Commands;
using global::Identity.Core.Models.Queries;
using global::Identity.Core.Services.Interfaces;

using Google.Protobuf.WellKnownTypes;

using Microsoft.AspNetCore.Authorization;

[Authorize]
public class IdentityService : Identity.IdentityBase
{
    private readonly ILogger<IdentityService> _logger;
    private readonly IUserService _userService;

    public IdentityService(ILogger<IdentityService> logger, IUserService service)
    {
        _logger = logger;
        _userService = service;
    }

    [AllowAnonymous]
    public override async Task<Empty> RegisterUser(RegisterUserRequest request, ServerCallContext context)
    {
        await _userService.RegisterUserAsync(new RegisterUserCommand { Email = request.Email, Password = request.Password });
        await _userService.LogInUserAsync(new LogInUserQuery { Username = request.Email, Password = request.Password });
        return new Empty();
    }

    [AllowAnonymous]
    public override async Task<Empty> Login(LoginRequest request, ServerCallContext context)
    {
        await _userService.LogInUserAsync(new LogInUserQuery { Username = request.Username, Password = request.Password });
        return new Empty();
    }
}