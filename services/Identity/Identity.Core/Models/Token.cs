namespace Identity.Core.Models;
public class Token
{
    public required string AccessToken { get; init; }
    public string TokenType { get; private set; }
    public required int ExpiresIn { get; init; }

    public Token(string tokenType = "Bearer")
    {
        TokenType = tokenType;
    }
}
