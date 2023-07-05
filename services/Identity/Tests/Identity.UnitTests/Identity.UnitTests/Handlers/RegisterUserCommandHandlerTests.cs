namespace Identity.UnitTests.Handlers;

using Identity.Core.Exceptions;
using Identity.Core.Handlers;
using Identity.Core.Models;
using Identity.Core.Models.Commands;
using Identity.UnitTests.Handlers.Helpers;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

public class RegisterUserCommandHandlerTests
{
    private readonly RegisterUserCommandHandler _handler;
    private readonly Mock<UserManager<ApplicationUser>> _mockUserManager = MockHelpers.UserManager;
    private readonly Fixture _fixture = new();

    public RegisterUserCommandHandlerTests()
    {
        var logger = Mock.Of<ILogger<RegisterUserCommandHandler>>();
        var passwordHasher = Mock.Of<IPasswordHasher<ApplicationUser>>();
        _handler = new RegisterUserCommandHandler(logger, _mockUserManager.Object, passwordHasher);
    }

    [Fact]
    public async Task HandleAsync_CreateUserResultFailed_NullReturned()
    {
        // Arrange
        _mockUserManager.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>()))
            .ReturnsAsync(IdentityResult.Failed());

        var command = _fixture.Create<RegisterUserCommand>();

        // Act
        var actual = async () => await _handler.HandleAsync(command);

        // Assert
        await actual.Should().ThrowAsync<RegisterUserException>();
    }

    [Fact]
    public async Task HandleAsync_CreateUserResultSuccess_NewUserReturned()
    {
        // Arrange
        _mockUserManager.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>()))
            .ReturnsAsync(IdentityResult.Success);

        var command = _fixture.Create<RegisterUserCommand>();

        // Act
        var actual = await _handler.HandleAsync(command);

        // Assert
        actual.Should().BeOfType<ApplicationUser>();
    }
}
