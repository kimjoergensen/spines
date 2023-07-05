namespace Identity.API.Services.Interfaces;

using System.ServiceModel;

using Identity.Core.Models.Requests;
using Identity.Core.Models.Responses;

[ServiceContract]
public interface IIdentityService
{
    /// <inheritdoc cref="IdentityService.Register(RegisterUserRequest)"/>
    public ValueTask<AuthenticateUserResponse> Register(RegisterUserRequest request);

    /// <inheritdoc cref="IdentityService.SignIn(AuthenticateUserRequest)"/>
    public ValueTask<AuthenticateUserResponse> SignIn(AuthenticateUserRequest request);

    /// <inheritdoc cref="IdentityService.SignOut(SignOutUserRequest)"/>
    ValueTask SignOut(SignOutUserRequest request);
}