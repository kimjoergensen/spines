namespace Identity.API.Services.Interfaces;

using System.ServiceModel;

using Identity.Core.Models.Requests;

[ServiceContract]
public interface IUserService
{
    public ValueTask RegisterUser(RegisterUserRequest request);
}