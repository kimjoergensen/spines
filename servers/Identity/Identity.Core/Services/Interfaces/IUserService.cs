namespace Identity.Core.Services.Interfaces;

using System.Threading.Tasks;

using Identity.Core.Models.Commands;

public interface IUserService
{

    /// <inheritdoc cref="UserService.RegisterUserAsync(RegisterUserCommand)"/>
    Task RegisterUserAsync(RegisterUserCommand command);
}
