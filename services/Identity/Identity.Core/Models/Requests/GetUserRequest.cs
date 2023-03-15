namespace Identity.Core.Models.Requests;

using Spines.Shared.Mediators;

public class GetUserRequest : IRequest
{
    public required string Username { get; init; }
}
