namespace Identity.Core.Models.Commands;
public class RegisterUserCommand
{
    public string Email { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
}
