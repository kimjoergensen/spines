namespace Identity.Core.Handlers;

using Identity.Core.Exceptions;
using Identity.Core.Models;
using Identity.Core.Models.Requests;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

using Spines.Shared.Mediators;

public class RegisterUserRequestHandler : IRequestHandler<RegisterUserRequest, ApplicationUser>
{
    private readonly ILogger _logger;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IPasswordHasher<ApplicationUser> _passwordHasher;

    public RegisterUserRequestHandler(ILogger<RegisterUserRequestHandler> logger, UserManager<ApplicationUser> userManager, IPasswordHasher<ApplicationUser> passwordHasher)
    {
        _logger = logger;
        _userManager = userManager;
        _passwordHasher = passwordHasher;
    }

    public async Task<ApplicationUser> HandleAsync(RegisterUserRequest request)
    {
        var user = new ApplicationUser { UserName = request.Email, Email = request.Email };
        user.PasswordHash = _passwordHasher.HashPassword(user, request.Password);
        var result = await _userManager.CreateAsync(user);
        return result.Succeeded ? user : throw new UserRegistrationException(request.Email);
    }
}
