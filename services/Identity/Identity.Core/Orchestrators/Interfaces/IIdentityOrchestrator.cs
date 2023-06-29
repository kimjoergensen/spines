namespace Identity.Core.Orchestrators.Interfaces;

using Identity.Core.Models;
using Identity.Core.Models.Requests;
using Identity.Core.Orchestrators;

public interface IIdentityOrchestrator
{
    /// <inheritdoc cref="IdentityOrchestrator.AuthenticateUserAsync(AuthenticateUserRequest)"/>
    Task<Token> AuthenticateUserAsync(AuthenticateUserRequest request);
}
