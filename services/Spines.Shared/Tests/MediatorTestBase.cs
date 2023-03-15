namespace Spines.Shared.Tests;

using AutoFixture;

using Moq;

using Spines.Shared.Mediators;

public class MediatorTestBase
{
    protected readonly Mock<IMediator> _mediatorMock = new();
    protected readonly Fixture _fixture = new();
}
