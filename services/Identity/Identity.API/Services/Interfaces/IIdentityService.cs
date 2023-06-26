namespace Identity.API.Services.Interfaces;

using System.Runtime.Serialization;
using System.ServiceModel;

[ServiceContract]
public interface IIdentityService
{
    public ValueTask<AuthenticateUserResponse> Authenticate(AuthenticateUserRequest request);
}

[DataContract]
public class AuthenticateUserRequest
{
    [DataMember(Order = 1)]
    public string Username { get; set; }

    [DataMember(Order = 2)]
    public string Password { get; set; }
}

[DataContract]
public class AuthenticateUserResponse
{
    [DataMember(Order = 1)]
    public string AccessToken { get; set; }

    [DataMember(Order = 2)]
    public TimeSpan Expires { get; set; }
}