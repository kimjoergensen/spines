namespace Identity.API.Services.Interfaces;

using System.Runtime.Serialization;
using System.ServiceModel;

[ServiceContract]
public interface IUserService
{
    public ValueTask RegisterUser(RegisterUserRequest request);
}

[DataContract]
public record RegisterUserRequest
{
    [DataMember(Order = 1)]
    public string Email { get; set; }

    [DataMember(Order = 2)]
    public string Password { get; set; }
}