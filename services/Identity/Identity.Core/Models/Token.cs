namespace Identity.Core.Models;

public class Token
{
    public required string AccessToken { get; init; }
    public required int ExpiresIn { get; init; }
    public string TokenType { get; private set; }

    public Token(string tokenType = "Bearer")
    {
        TokenType = tokenType;
    }
}
