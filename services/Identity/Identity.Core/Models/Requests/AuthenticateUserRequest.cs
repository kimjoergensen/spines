namespace Identity.Core.Models.Requests;

using Spines.Shared.Mediators;

public class AuthenticateUserRequest : IRequest
{
    public required ApplicationUser User { get; init; }
    public required string Password { get; init; }
}
