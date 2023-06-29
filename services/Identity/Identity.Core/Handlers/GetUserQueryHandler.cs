namespace Identity.Core.Handlers;

using Identity.Core.Models;
using Identity.Core.Models.Queries;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

using Spines.Shared.Mediators;

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, ApplicationUser>
{
    private readonly ILogger _logger;
    private readonly UserManager<ApplicationUser> _userManager;

    public GetUserQueryHandler(ILogger<GetUserQueryHandler> logger, UserManager<ApplicationUser> userManager)
    {
        _logger = logger;
        _userManager = userManager;
    }

    public async ValueTask<ApplicationUser?> HandleAsync(GetUserQuery request) =>
        await _userManager.FindByNameAsync(request.Username);
}
