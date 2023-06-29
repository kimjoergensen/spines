namespace Identity.UnitTests.Handlers;

using Identity.Core.Exceptions;
using Identity.Core.Handlers;
using Identity.Core.Models;
using Identity.Core.Models.Queries;
using Identity.Core.Options;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

public class AuthenticateUserQueryHandlerTest
{
    private readonly AuthenticateUserQueryHandler _handler;
    private readonly Mock<SignInManager<ApplicationUser>> _mockSignInManager = MockSignInManager;
    private readonly Fixture _fixture = new();

    public AuthenticateUserQueryHandlerTest()
    {
        var settings = new AuthenticationOptions
        {
            SigninKey = "a8C123CbA8bE506077A134C265b3F1759FCa7D8c29dEDC2533D04Bf5323C1A2E",
            Issuer = "https://localhost:5001",
            Audience = "https://localhost:5001",
            ExpiresIn = 30
        };

        var logger = Mock.Of<ILogger<AuthenticateUserQueryHandler>>();
        _handler = new AuthenticateUserQueryHandler(logger, _mockSignInManager.Object, Options.Create(settings));
    }

    [Fact]
    public async Task HandleAsync_SignInResultFailed_AuthenticateUserExceptionThrown()
    {
        // Arrange
        _mockSignInManager.Setup(x => x.PasswordSignInAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>(), true, false))
            .ReturnsAsync(SignInResult.Failed);

        var query = _fixture.Create<AuthenticateUserQuery>();

        // Act
        var actual = async () => await _handler.HandleAsync(query);

        // Assert
        await actual.Should().ThrowAsync<AuthenticateUserException>();
    }

    [Fact]
    public async Task HandleAsync_SignInResultNotAllowed_AuthenticateUserExceptionThrown()
    {
        // Arrange
        _mockSignInManager.Setup(x => x.PasswordSignInAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>(), true, false))
            .ReturnsAsync(SignInResult.NotAllowed);

        var query = _fixture.Create<AuthenticateUserQuery>();

        // Act
        var actual = async () => await _handler.HandleAsync(query);

        // Assert
        await actual.Should().ThrowAsync<AuthenticateUserException>();
    }

    [Fact]
    public async Task HandleAsync_SignInResultLockedOut_AuthenticateUserExceptionThrown()
    {
        // Arrange
        _mockSignInManager.Setup(x => x.PasswordSignInAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>(), true, false))
            .ReturnsAsync(SignInResult.LockedOut);

        var query = _fixture.Create<AuthenticateUserQuery>();

        // Act
        var actual = async () => await _handler.HandleAsync(query);

        // Assert
        await actual.Should().ThrowAsync<AuthenticateUserException>();
    }

    [Fact]
    public async Task HandleAsync_SignInResultTwoFactorRequired_AuthenticateUserExceptionThrown()
    {
        // Arrange
        _mockSignInManager.Setup(x => x.PasswordSignInAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>(), true, false))
            .ReturnsAsync(SignInResult.TwoFactorRequired);

        var query = _fixture.Create<AuthenticateUserQuery>();

        // Act
        var actual = async () => await _handler.HandleAsync(query);

        // Assert
        await actual.Should().ThrowAsync<AuthenticateUserException>();
    }

    [Fact]
    public async Task HandleAsync_SignInResultSuccess_TokenReturned()
    {
        // Arrange
        _mockSignInManager.Setup(x => x.PasswordSignInAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>(), true, false))
            .ReturnsAsync(SignInResult.Success);

        var query = _fixture.Create<AuthenticateUserQuery>();

        // Act
        var actual = await _handler.HandleAsync(query);

        // Assert
        actual.Should().BeOfType<Token>();
    }

    private static Mock<UserManager<ApplicationUser>> MockUserManager =>
        new(Mock.Of<IUserStore<ApplicationUser>>(),
            /* IOptions<IdentityOptions> optionsAccessor */null,
            /* IPasswordHasher<TUser> passwordHasher */null,
            /* IEnumerable<IUserValidator<TUser>> userValidators */null,
            /* IEnumerable<IPasswordValidator<TUser>> passwordValidators */null,
            /* ILookupNormalizer keyNormalizer */null,
            /* IdentityErrorDescriber errors */null,
            /* IServiceProvider services */null,
            /* ILogger<UserManager<TUser>> logger */null);

    private static Mock<SignInManager<ApplicationUser>> MockSignInManager =>
        new(MockUserManager.Object,
            /* IHttpContextAccessor contextAccessor */Mock.Of<IHttpContextAccessor>(),
            /* IUserClaimsPrincipalFactory<TUser> claimsFactory */Mock.Of<IUserClaimsPrincipalFactory<ApplicationUser>>(),
            /* IOptions<IdentityOptions> optionsAccessor */null,
            /* ILogger<SignInManager<TUser>> logger */null,
            /* IAuthenticationSchemeProvider schemes */null);
}
