namespace Identity.UnitTests.Orchestrators;

using Identity.Core.Exceptions;
using Identity.Core.Models;
using Identity.Core.Models.Queries;
using Identity.Core.Models.Requests;
using Identity.Core.Orchestrators;
using Identity.Core.Orchestrators.Interfaces;

using Microsoft.Extensions.Logging;

using Spines.Shared.Tests;

public class IdentityOrchestratorTests : OrchestratorTestBase
{
    private readonly IIdentityOrchestrator _identityMediator;

    public IdentityOrchestratorTests()
    {
        var logger = Mock.Of<ILogger<IdentityOrchestrator>>();
        _identityMediator = new IdentityOrchestrator(logger, MediatorMock.Object);
    }

    #region AuthenticateUserAsync
    [Fact]
    public async Task AuthenticateUserAsync_NotFoundException_ExceptionRethrown()
    {
        // Arrange
        MediatorMock.Setup(x => x.InvokeAsync<ApplicationUser>(It.IsAny<GetUserQuery>()))
            .ReturnsAsync(null as ApplicationUser);

        // Act
        var actual = async () => await _identityMediator.AuthenticateUserAsync(Fixture.Create<AuthenticateUserRequest>());

        // Assert
        await actual.Should().ThrowAsync<AuthenticateUserException>();
    }

    [Fact]
    public async Task AuthenticateUserAsync_UserNotAuthenticated_ReturnsNull()
    {
        // Arrange
        var user = Fixture.Create<ApplicationUser>();
        MediatorMock.Setup(x => x.InvokeAsync<ApplicationUser>(It.IsAny<GetUserQuery>()))
            .ReturnsAsync(user);

        MediatorMock.Setup(x => x.InvokeAsync<Token>(It.IsAny<AuthenticateUserQuery>()))
            .ThrowsAsync(new AuthenticateUserException(user.UserName!));

        // Act
        var actual = async () => await _identityMediator.AuthenticateUserAsync(Fixture.Create<AuthenticateUserRequest>());

        // Assert
        await actual.Should().ThrowAsync<AuthenticateUserException>();
    }

    [Fact]
    public async Task AuthenticateUserAsync_UserAuthenticated_ReturnsToken()
    {
        // Arrange
        var user = Fixture.Create<ApplicationUser>();
        var expected = Fixture.Create<Token>();

        MediatorMock.Setup(x => x.InvokeAsync<ApplicationUser>(It.IsAny<GetUserQuery>()))
            .ReturnsAsync(user);

        MediatorMock.Setup(x => x.InvokeAsync<Token>(It.IsAny<AuthenticateUserQuery>()))
            .ReturnsAsync(expected);

        // Act
        var actual = await _identityMediator.AuthenticateUserAsync(Fixture.Create<AuthenticateUserRequest>());

        // Assert
        actual.Should().BeEquivalentTo(expected);
    }
    #endregion
}
