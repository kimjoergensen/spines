namespace Identity.Core.Handlers;

using Identity.Core.Exceptions;
using Identity.Core.Models;
using Identity.Core.Models.Commands;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

using Spines.Shared.Mediators;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, ApplicationUser>
{
    private readonly ILogger _logger;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IPasswordHasher<ApplicationUser> _passwordHasher;

    public RegisterUserCommandHandler(
        ILogger<RegisterUserCommandHandler> logger,
        UserManager<ApplicationUser> userManager,
        IPasswordHasher<ApplicationUser> passwordHasher)
    {
        _logger = logger;
        _userManager = userManager;
        _passwordHasher = passwordHasher;
    }

    public async ValueTask<ApplicationUser?> HandleAsync(RegisterUserCommand request)
    {
        var user = new ApplicationUser { UserName = request.Email, Email = request.Email };
        user.PasswordHash = _passwordHasher.HashPassword(user, request.Password);
        var result = await _userManager.CreateAsync(user);
        _logger.LogInformation("User created result for '{user}' {result}", request.Email, result.ToString());
        return result.Succeeded ? user : throw new UserRegistrationException(request.Email, result.Errors);
    }
}
