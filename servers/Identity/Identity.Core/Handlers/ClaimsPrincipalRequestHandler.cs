namespace Identity.Core.Handlers;

using System.Security.Claims;

using Identity.Core.Models;
using Identity.Core.Models.Requests;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

using Spines.Shared.Mediators;

public class ClaimsPrincipalRequestHandler : IRequestHandler<GetClaimsPrincipalRequest, ClaimsPrincipal>
{
    private readonly ILogger _logger;
    private readonly UserManager<ApplicationUser> _userManager;

    public ClaimsPrincipalRequestHandler(ILogger<ClaimsPrincipalRequestHandler> logger, UserManager<ApplicationUser> userManager)
    {
        _logger = logger;
        _userManager = userManager;
    }

    public async Task<ClaimsPrincipal> HandleAsync(GetClaimsPrincipalRequest request)
    {
        var claims = await _userManager.GetClaimsAsync(request.User);
        var identity = new ClaimsIdentity(claims, "Bearer");
        return new ClaimsPrincipal(identity);
    }
}
