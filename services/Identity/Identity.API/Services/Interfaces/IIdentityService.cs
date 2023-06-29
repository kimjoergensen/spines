namespace Identity.API.Services.Interfaces;

using System.ServiceModel;

using Identity.Core.Models.Requests;
using Identity.Core.Models.Responses;

[ServiceContract]
public interface IIdentityService
{
    public ValueTask<AuthenticateUserResponse> Authenticate(AuthenticateUserRequest request);
}