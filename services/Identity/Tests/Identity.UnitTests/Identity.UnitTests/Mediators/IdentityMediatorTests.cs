namespace Identity.UnitTests.Mediators;

using Identity.Core.Models;
using Identity.Core.Models.Queries;
using Identity.Core.Models.Requests;
using Identity.Core.Services;
using Identity.Core.Services.Interfaces;

using Microsoft.Extensions.Logging;

using Spines.Shared.Tests;

public class IdentityMediatorTests : MediatorTestBase
{
    private readonly IIdentityMediator _identityMediator;

    public IdentityMediatorTests()
    {
        var logger = Mock.Of<ILogger<IdentityMediator>>();
        _identityMediator = new IdentityMediator(logger, MediatorMock.Object);
    }

    #region AuthenticateUserAsync
    [Fact]
    public async Task AuthenticateUserAsync_UserNotFound_ReturnsNull()
    {
        // Arrange
        MediatorMock.Setup(x => x.InvokeAsync<ApplicationUser>(It.IsAny<GetUserRequest>()))
            .ReturnsAsync(null as ApplicationUser);

        // Act
        var actual = await _identityMediator.AuthenticateUserAsync(Fixture.Create<AuthenticateUserQuery>());

        // Assert
        actual.Should().BeNull();
    }

    [Fact]
    public async Task AuthenticateUserAsync_UserNotAuthenticated_ReturnsNull()
    {
        // Arrange
        MediatorMock.Setup(x => x.InvokeAsync<ApplicationUser>(It.IsAny<GetUserRequest>()))
            .ReturnsAsync(Fixture.Create<ApplicationUser>());

        MediatorMock.Setup(x => x.InvokeAsync<Token>(It.IsAny<AuthenticateUserRequest>()))
            .ReturnsAsync(null as Token);

        // Act
        var actual = await _identityMediator.AuthenticateUserAsync(Fixture.Create<AuthenticateUserQuery>());

        // Assert
        actual.Should().BeNull();
    }

    [Fact]
    public async Task AuthenticateUserAsync_UserAuthenticated_ReturnsToken()
    {
        // Arrange
        var expected = Fixture.Create<Token>();

        MediatorMock.Setup(x => x.InvokeAsync<ApplicationUser>(It.IsAny<GetUserRequest>()))
            .ReturnsAsync(Fixture.Create<ApplicationUser>());

        MediatorMock.Setup(x => x.InvokeAsync<Token>(It.IsAny<AuthenticateUserRequest>()))
            .ReturnsAsync(expected);

        // Act
        var actual = await _identityMediator.AuthenticateUserAsync(Fixture.Create<AuthenticateUserQuery>());

        // Assert
        actual.Should().BeEquivalentTo(expected);
    }
    #endregion
}
