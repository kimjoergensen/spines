namespace Identity.Core.Mediators.Interfaces;

using Identity.Core.Mediators;

using Identity.Core.Models;
using Identity.Core.Models.Queries;

public interface IIdentityMediator
{
    /// <inheritdoc cref="IdentityMediator.AuthenticateUserAsync(AuthenticateUserQuery)"/>
    Task<Token> AuthenticateUserAsync(AuthenticateUserQuery query);
}
