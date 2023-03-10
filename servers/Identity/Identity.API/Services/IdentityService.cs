namespace Identity.API.Services;

using Grpc.Core;

public class IdentityService : Identity.IdentityBase
{
    private readonly ILogger<IdentityService> _logger;
    public IdentityService(ILogger<IdentityService> logger)
    {
        _logger = logger;
    }

    public override Task<HelloReply> Greet(HelloRequest request, ServerCallContext context)
    {
        return Task.FromResult(new HelloReply
        {
            Message = "Hello " + request.Name
        });
    }
}
