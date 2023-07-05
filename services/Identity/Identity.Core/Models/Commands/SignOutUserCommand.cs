namespace Identity.Core.Models.Commands;

using Spines.Shared.Mediators;

public record SignOutUserCommand : IRequest
{
    public required string Email { get; init; }
}
