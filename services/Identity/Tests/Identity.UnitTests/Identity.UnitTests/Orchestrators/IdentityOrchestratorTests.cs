namespace Identity.UnitTests.Orchestrators;

using Identity.Core.Exceptions;
using Identity.Core.Models;
using Identity.Core.Models.Queries;
using Identity.Core.Models.Requests;
using Identity.Core.Orchestrators;
using Identity.Core.Orchestrators.Interfaces;

using Microsoft.Extensions.Logging;

using Spines.Shared.Mediators;

public class IdentityOrchestratorTests
{
    private readonly IIdentityOrchestrator _identityMediator;
    private readonly Mock<IMediator> _mockMediator = new();
    private readonly Fixture _fixture = new();

    public IdentityOrchestratorTests()
    {
        var logger = Mock.Of<ILogger<IdentityOrchestrator>>();
        _identityMediator = new IdentityOrchestrator(logger, _mockMediator.Object);
    }

    #region AuthenticateUserAsync
    [Fact]
    public async Task AuthenticateUserAsync_NotFoundException_ExceptionRethrown()
    {
        // Arrange
        _mockMediator.Setup(x => x.InvokeAsync<ApplicationUser>(It.IsAny<GetUserQuery>()))
            .ReturnsAsync(null as ApplicationUser);

        // Act
        var actual = async () => await _identityMediator.AuthenticateUserAsync(_fixture.Create<AuthenticateUserRequest>());

        // Assert
        await actual.Should().ThrowAsync<AuthenticateUserException>();
    }

    [Fact]
    public async Task AuthenticateUserAsync_UserNotAuthenticated_ReturnsNull()
    {
        // Arrange
        var user = _fixture.Create<ApplicationUser>();
        _mockMediator.Setup(x => x.InvokeAsync<ApplicationUser>(It.IsAny<GetUserQuery>()))
            .ReturnsAsync(user);

        _mockMediator.Setup(x => x.InvokeAsync<Token>(It.IsAny<AuthenticateUserQuery>()))
            .ThrowsAsync(new AuthenticateUserException(user.UserName!));

        // Act
        var actual = async () => await _identityMediator.AuthenticateUserAsync(_fixture.Create<AuthenticateUserRequest>());

        // Assert
        await actual.Should().ThrowAsync<AuthenticateUserException>();
    }

    [Fact]
    public async Task AuthenticateUserAsync_UserAuthenticated_ReturnsToken()
    {
        // Arrange
        var user = _fixture.Create<ApplicationUser>();
        var expected = _fixture.Create<Token>();

        _mockMediator.Setup(x => x.InvokeAsync<ApplicationUser>(It.IsAny<GetUserQuery>()))
            .ReturnsAsync(user);

        _mockMediator.Setup(x => x.InvokeAsync<Token>(It.IsAny<AuthenticateUserQuery>()))
            .ReturnsAsync(expected);

        // Act
        var actual = await _identityMediator.AuthenticateUserAsync(_fixture.Create<AuthenticateUserRequest>());

        // Assert
        actual.Should().BeEquivalentTo(expected);
    }
    #endregion
}
