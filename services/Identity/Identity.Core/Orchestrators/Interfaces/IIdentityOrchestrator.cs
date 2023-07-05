namespace Identity.Core.Orchestrators.Interfaces;

using Identity.Core.Models;
using Identity.Core.Models.Requests;
using Identity.Core.Orchestrators;

public interface IIdentityOrchestrator
{
    /// <inheritdoc cref="IdentityOrchestrator.RegisterUserAsync(RegisterUserRequest)"/>
    ValueTask<Token> RegisterUserAsync(RegisterUserRequest command);

    /// <inheritdoc cref="IdentityOrchestrator.AuthenticateUserAsync(AuthenticateUserRequest)"/>
    ValueTask<Token> AuthenticateUserAsync(AuthenticateUserRequest request);

    /// <inheritdoc cref="IdentityOrchestrator.SignOutUserAsync(SignOutUserRequest)"/>
    ValueTask SignOutUserAsync(SignOutUserRequest request);
}
