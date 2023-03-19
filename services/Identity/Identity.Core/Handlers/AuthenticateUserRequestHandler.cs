namespace Identity.Core.Handlers;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Identity.Core.Exceptions;
using Identity.Core.Models;
using Identity.Core.Models.Requests;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using Spines.Shared.Mediators;

public class AuthenticateUserRequestHandler : IRequestHandler<AuthenticateUserRequest, Token>
{
    private readonly ILogger _logger;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly Core.Options.AuthenticationOptions _options;

    public AuthenticateUserRequestHandler(
        ILogger<AuthenticateUserRequestHandler> logger, SignInManager<ApplicationUser> signInManager, IOptions<Core.Options.AuthenticationOptions> options)
    {
        _logger = logger;
        _signInManager = signInManager;
        _options = options.Value;
    }

    public async ValueTask<Token> HandleAsync(AuthenticateUserRequest request)
    {
        var loginResult = await _signInManager.PasswordSignInAsync(request.User, request.Password, true, false);
        if (!loginResult.Succeeded || loginResult.IsNotAllowed || loginResult.IsLockedOut)
            throw new AuthenticateUserException(request.User);

        var token = GenerateToken(request.User);
        return new Token() { AccessToken = token, ExpiresIn = _options.ExpiresIn };
    }

    private string GenerateToken(ApplicationUser user)
    {
        if (user.UserName is null) throw new AuthenticateUserException(user);

        var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_options.SigninKey));
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.UserName)
            }),
            Expires = DateTime.UtcNow.AddMinutes(30),
            Issuer = _options.Issuer,
            Audience = _options.Audience,
            SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
        };

        return tokenHandler.CreateEncodedJwt(tokenDescriptor);
    }
}

