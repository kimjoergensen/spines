namespace Identity.Core.Models.Commands;

using Spines.Shared.Mediators;

public record RegisterUserCommand : IRequest
{
    public required string Email { get; init; }
    public required string Password { get; init; }
}
