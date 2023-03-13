namespace Identity.Core.Models.Requests;

using Spines.Shared.Mediators;

public class LogInUserRequest : IRequest
{
    public ApplicationUser User { get; private set; }
    public string Password { get; private set; }

    public LogInUserRequest(ApplicationUser user, string password)
    {
        User = user;
        Password = password;
    }
}
