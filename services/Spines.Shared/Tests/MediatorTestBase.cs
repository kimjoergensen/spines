namespace Spines.Shared.Tests;

using AutoFixture;

using Moq;

using Spines.Shared.Mediators;

public class MediatorTestBase
{
    protected Mock<IMediator> MediatorMock { get; } = new();
    protected Fixture Fixture { get; } = new();
}
