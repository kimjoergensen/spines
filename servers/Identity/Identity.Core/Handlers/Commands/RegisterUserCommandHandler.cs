namespace Identity.Core.Handlers.Commands;
using System.Threading.Tasks;

using Identity.Core.Models;
using Identity.Core.Models.Commands;

using Microsoft.AspNetCore.Identity;

using Spines.Shared.Mediators;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IPasswordHasher<ApplicationUser> _passwordHasher;

    public RegisterUserCommandHandler(UserManager<ApplicationUser> userManager, IPasswordHasher<ApplicationUser> passwordHasher)
    {
        _userManager = userManager;
        _passwordHasher = passwordHasher;
    }

    public async Task HandleAsync(RegisterUserCommand request)
    {
        var user = new ApplicationUser { UserName = request.Email, Email = request.Email };
        user.PasswordHash = _passwordHasher.HashPassword(user, request.Password);
        await _userManager.CreateAsync(user);
    }
}
