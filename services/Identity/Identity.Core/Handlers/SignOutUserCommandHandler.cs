namespace Identity.Core.Handlers;
using System.Threading.Tasks;

using Identity.Core.Models;
using Identity.Core.Models.Commands;

using Microsoft.AspNetCore.Identity;

using Microsoft.Extensions.Logging;

using Spines.Shared.Mediators;

public class SignOutUserCommandHandler : IRequestHandler<SignOutUserCommand>
{
    private readonly ILogger _logger;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public SignOutUserCommandHandler(ILogger<SignOutUserCommandHandler> logger, SignInManager<ApplicationUser> signInManager)
    {
        _logger = logger;
        _signInManager = signInManager;
    }

    public async ValueTask HandleAsync(SignOutUserCommand request)
    {
        await _signInManager.SignOutAsync();
    }
}
