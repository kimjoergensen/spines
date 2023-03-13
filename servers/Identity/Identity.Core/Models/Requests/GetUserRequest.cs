namespace Identity.Core.Models.Requests;

using Spines.Shared.Mediators;

public class GetUserRequest : IRequest
{
    public string Username { get; private set; }

    public GetUserRequest(string username)
    {
        Username = username;
    }
}
