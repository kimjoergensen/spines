namespace Identity.Core.Services.Interfaces;
using System.Threading.Tasks;

using Identity.Core.Models.Commands;

public interface IUserService
{
    Task RegisterUserAsync(RegisterUserCommand command);
}
