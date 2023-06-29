namespace Identity.Core.Models.Queries;

using Spines.Shared.Mediators;

public record AuthenticateUserQuery : IRequest
{
    public required ApplicationUser User { get; init; }
    public required string Password { get; init; }
}
