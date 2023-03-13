namespace Identity.Core.Models.Queries;
public class AuthenticateUserQuery
{
    public required string Username { get; set; }
    public required string Password { get; set; }
}
