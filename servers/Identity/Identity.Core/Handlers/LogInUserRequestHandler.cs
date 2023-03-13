namespace Identity.Core.Handlers;

using Identity.Core.Exceptions;
using Identity.Core.Models;
using Identity.Core.Models.Requests;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

using Spines.Shared.Mediators;

public class LogInUserRequestHandler : IRequestHandler<LogInUserRequest>
{
    private readonly ILogger _logger;
    private readonly IPasswordHasher<ApplicationUser> _passwordHasher;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public LogInUserRequestHandler(
        ILogger<LogInUserRequestHandler> logger, IPasswordHasher<ApplicationUser> passwordHasher, SignInManager<ApplicationUser> signInManager)
    {
        _logger = logger;
        _passwordHasher = passwordHasher;
        _signInManager = signInManager;
    }

    public async Task HandleAsync(LogInUserRequest request)
    {
        var passwordVerification = _passwordHasher.VerifyHashedPassword(request.User, request.User.PasswordHash!, request.Password);
        if (passwordVerification is PasswordVerificationResult.Failed)
            throw new UserLoginException(request.User);
        await _signInManager.SignInAsync(request.User, true);
    }
}
