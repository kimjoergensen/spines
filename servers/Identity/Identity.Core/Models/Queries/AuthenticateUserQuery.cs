namespace Identity.Core.Models.Queries;
public class AuthenticateUserQuery
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
