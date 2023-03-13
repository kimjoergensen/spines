namespace Identity.Core.Models.Commands;
public class RegisterUserCommand
{
    public required string Email { get; init; }
    public required string Password { get; init; }
}
