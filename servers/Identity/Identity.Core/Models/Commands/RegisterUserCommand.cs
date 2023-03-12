namespace Identity.Core.Models.Commands;

using Spines.Shared.Mediators;

public class RegisterUserCommand : IRequest
{
    public string Email { get; private set; }
    public string Password { get; private set; }

    public RegisterUserCommand(string email, string password)
    {
        Email = email;
        Password = password;
    }
}
