namespace Identity.Core.Handlers;

using Identity.Core.Models;
using Identity.Core.Models.Requests;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

using Spines.Shared.Mediators;

public class GetUserRequestHandler : IRequestHandler<GetUserRequest, ApplicationUser>
{
    private readonly ILogger _logger;
    private readonly UserManager<ApplicationUser> _userManager;

    public GetUserRequestHandler(ILogger<GetUserRequestHandler> logger, UserManager<ApplicationUser> userManager)
    {
        _logger = logger;
        _userManager = userManager;
    }

    public async ValueTask<ApplicationUser?> HandleAsync(GetUserRequest request) =>
        await _userManager.FindByNameAsync(request.Username);
}
