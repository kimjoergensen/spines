namespace Identity.Core.Orchestrators.Interfaces;

using System.Threading.Tasks;

using Identity.Core.Models;
using Identity.Core.Models.Requests;
using Identity.Core.Orchestrators;

public interface IUserOrchestrator
{

    /// <inheritdoc cref="UserOrchestrator.RegisterUserAsync(RegisterUserRequest)"/>
    Task<ApplicationUser?> RegisterUserAsync(RegisterUserRequest command);
}
