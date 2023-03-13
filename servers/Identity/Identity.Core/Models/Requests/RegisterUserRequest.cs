namespace Identity.Core.Models.Requests;

using Spines.Shared.Mediators;

public class RegisterUserRequest : IRequest
{
    public string Email { get; private set; }
    public string Password { get; private set; }

    public RegisterUserRequest(string email, string password)
    {
        Email = email;
        Password = password;
    }
}
