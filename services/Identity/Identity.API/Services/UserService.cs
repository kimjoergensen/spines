namespace Identity.API.Services;

using System.Threading.Tasks;

using Grpc.Core;

using Identity.API.Services.Interfaces;
using Identity.Core.Exceptions;
using Identity.Core.Models.Requests;
using Identity.Core.Orchestrators.Interfaces;

public class UserService : IUserService
{
    private readonly ILogger _logger;
    private readonly IUserOrchestrator _userOrchestrator;

    public UserService(ILogger<UserService> logger, IUserOrchestrator orchestrator)
    {
        _logger = logger;
        _userOrchestrator = orchestrator;
    }

    public async ValueTask RegisterUser(RegisterUserRequest request)
    {
        try
        {
            await _userOrchestrator.RegisterUserAsync(request);
        }
        catch (UserRegistrationException ex)
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, ex.Message));
        }
    }
}
