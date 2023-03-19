namespace Identity.Core.Services.Interfaces;

using System.Threading.Tasks;

using Identity.Core.Models;
using Identity.Core.Models.Commands;

public interface IUserMediator
{

    /// <inheritdoc cref="UserMediator.RegisterUserAsync(RegisterUserCommand)"/>
    Task<ApplicationUser?> RegisterUserAsync(RegisterUserCommand command);
}
