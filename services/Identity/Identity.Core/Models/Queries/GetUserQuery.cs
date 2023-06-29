namespace Identity.Core.Models.Queries;

using Spines.Shared.Mediators;

public record GetUserQuery : IRequest
{
    public required string Username { get; init; }
}
