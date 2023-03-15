namespace Identity.Core.Options;
public class AuthenticationOptions
{
    public const string Configuration = "Authentication";

    public string SigninKey { get; init; } = string.Empty;
    public string Issuer { get; init; } = string.Empty;
    public string Audience { get; init; } = string.Empty;
    public int ExpiresIn { get; init; }
}
