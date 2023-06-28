namespace Identity.API.Services.Interfaces;

using System.Runtime.Serialization;
using System.ServiceModel;

[ServiceContract]
public interface IIdentityService
{
    public ValueTask<AuthenticateUserResponse> Authenticate(AuthenticateUserRequest request);
}

[DataContract]
public record AuthenticateUserRequest
{
    [DataMember(Order = 1)]
    public required string Username { get; set; }

    [DataMember(Order = 2)]
    public required string Password { get; set; }
}

[DataContract]
public class AuthenticateUserResponse
{
    [DataMember(Order = 1)]
    public required string AccessToken { get; set; }

    [DataMember(Order = 2)]
    public required TimeSpan Expires { get; set; }
}