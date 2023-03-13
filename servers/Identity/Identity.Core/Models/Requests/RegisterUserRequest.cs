namespace Identity.Core.Models.Requests;

using Spines.Shared.Mediators;

public class RegisterUserRequest : IRequest
{
    public required string Email { get; init; }
    public required string Password { get; init; }
}
