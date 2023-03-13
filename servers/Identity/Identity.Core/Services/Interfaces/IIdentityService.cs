namespace Identity.Core.Services.Interfaces;
using System.Threading.Tasks;

using Identity.Core.Models;
using Identity.Core.Models.Queries;

public interface IIdentityService
{
    /// <inheritdoc cref="UserService.AuthenticateUserAsync(AuthenticateUserQuery)"/>
    Task<Token> AuthenticateUserAsync(AuthenticateUserQuery query);
}
