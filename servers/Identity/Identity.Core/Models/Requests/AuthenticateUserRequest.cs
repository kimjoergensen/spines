namespace Identity.Core.Models.Requests;

using Spines.Shared.Mediators;

public class AuthenticateUserRequest : IRequest
{
    public ApplicationUser User { get; private set; }
    public string Password { get; private set; }

    public AuthenticateUserRequest(ApplicationUser user, string password)
    {
        User = user;
        Password = password;
    }
}
