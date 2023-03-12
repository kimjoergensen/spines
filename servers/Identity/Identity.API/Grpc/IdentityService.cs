namespace Identity.API.Grpc;

using global::Grpc.Core;
using global::Identity.Core.Models.Commands;
using global::Identity.Core.Services.Interfaces;

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
    public override async Task<EmptyReply> RegisterUser(RegisterUserRequest request, ServerCallContext context)
    {
        _logger.LogInformation("Registering user with email {email}..", request.Email);
        await _userService.RegisterUserAsync(new RegisterUserCommand(request.Email, request.Password));
        return new EmptyReply();
    }
}