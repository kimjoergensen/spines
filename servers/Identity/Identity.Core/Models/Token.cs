namespace Identity.Core.Models;
public class Token
{
    public string AccessToken { get; private set; }
    public string TokenType { get; private set; }
    public int ExpiresIn { get; private set; }

    public Token(string accessToken, int expiresIn, string tokenType = "Bearer")
    {
        AccessToken = accessToken;
        TokenType = tokenType;
        ExpiresIn = expiresIn;
    }
}
