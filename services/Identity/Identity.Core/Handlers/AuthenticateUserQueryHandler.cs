namespace Identity.Core.Handlers;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Identity.Core.Exceptions;
using Identity.Core.Models;
using Identity.Core.Models.Queries;
using Identity.Core.Options;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using Spines.Shared.Mediators;

public class AuthenticateUserQueryHandler : IRequestHandler<AuthenticateUserQuery, Token>
{
    private readonly ILogger _logger;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly AuthenticationOptions _options;

    public AuthenticateUserQueryHandler(
        ILogger<AuthenticateUserQueryHandler> logger,
        SignInManager<ApplicationUser> signInManager,
        IOptions<AuthenticationOptions> options)
    {
        _logger = logger;
        _signInManager = signInManager;
        _options = options.Value;
    }

    public async ValueTask<Token?> HandleAsync(AuthenticateUserQuery request)
    {
        var loginResult = await _signInManager.PasswordSignInAsync(request.User, request.Password, true, false);
        _logger.LogInformation("Login result for user '{user}' {result}.", request.User.UserName, loginResult.ToString());

        if (!loginResult.Succeeded || loginResult.IsNotAllowed || loginResult.IsLockedOut || loginResult.RequiresTwoFactor)
            throw new AuthenticateUserException(request.User.UserName!);

        var token = GenerateToken(request.User);
        return new Token() { AccessToken = token, ExpiresIn = _options.ExpiresIn };
    }

    private string GenerateToken(ApplicationUser user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_options.SigninKey));
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.UserName!)
            }),
            Expires = DateTime.UtcNow.AddMinutes(_options.ExpiresIn),
            Issuer = _options.Issuer,
            Audience = _options.Audience,
            SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
        };

        return tokenHandler.CreateEncodedJwt(tokenDescriptor);
    }
}

