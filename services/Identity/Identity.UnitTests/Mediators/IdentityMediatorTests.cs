namespace Identity.UnitTests.Mediators;

using AutoFixture;

using FluentAssertions;

using Identity.Core.Models;
using Identity.Core.Models.Queries;
using Identity.Core.Models.Requests;
using Identity.Core.Services;

using Microsoft.Extensions.Logging;

using Moq;

using Spines.Shared.Tests;

public class IdentityMediatorTests : MediatorTestBase
{
    private readonly IdentityMediator _identityMediator;

    public IdentityMediatorTests()
    {
        var logger = Mock.Of<ILogger<IdentityMediator>>();
        _identityMediator = new IdentityMediator(logger, _mediatorMock.Object);
    }

    #region AuthenticateUserAsync
    [Fact]
    public async Task AuthenticateUserAsync_UserNotFound_ReturnsNull()
    {
        // Arrange
        _mediatorMock.Setup(x => x.InvokeAsync<ApplicationUser>(It.IsAny<GetUserRequest>()))
            .ReturnsAsync(null as ApplicationUser);

        // Act
        var actual = await _identityMediator.AuthenticateUserAsync(_fixture.Create<AuthenticateUserQuery>());

        // Assert
        actual.Should().BeNull();
    }

    [Fact]
    public async Task AuthenticateUserAsync_UserNotAuthenticated_ReturnsNull()
    {
        // Arrange
        _mediatorMock.Setup(x => x.InvokeAsync<ApplicationUser>(It.IsAny<GetUserRequest>()))
            .ReturnsAsync(_fixture.Create<ApplicationUser>());

        _mediatorMock.Setup(x => x.InvokeAsync<Token>(It.IsAny<AuthenticateUserRequest>()))
            .ReturnsAsync(null as Token);

        // Act
        var actual = await _identityMediator.AuthenticateUserAsync(_fixture.Create<AuthenticateUserQuery>());

        // Assert
        actual.Should().BeNull();
    }

    [Fact]
    public async Task AuthenticateUserAsync_UserAuthenticated_ReturnsToken()
    {
        // Arrange
        var expected = _fixture.Create<Token>();

        _mediatorMock.Setup(x => x.InvokeAsync<ApplicationUser>(It.IsAny<GetUserRequest>()))
            .ReturnsAsync(_fixture.Create<ApplicationUser>());

        _mediatorMock.Setup(x => x.InvokeAsync<Token>(It.IsAny<AuthenticateUserRequest>()))
            .ReturnsAsync(expected);

        // Act
        var actual = await _identityMediator.AuthenticateUserAsync(_fixture.Create<AuthenticateUserQuery>());

        // Assert
        actual.Should().BeEquivalentTo(expected);
    }
    #endregion
}
