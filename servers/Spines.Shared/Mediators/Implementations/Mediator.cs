namespace Spines.Shared.Mediators.Implementations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Spines.Shared.Mediators;

internal class Mediator : IMediator
{
    private readonly ILogger _logger;
    private readonly IServiceProvider _serviceProvider;

    public Mediator(ILogger<Mediator> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    public async Task InvokeAsync(IRequest request)
    {
        // A new scope must be created to resolve scoped services.
        using var scope = _serviceProvider.CreateScope();

        // Resolve closed handler type.
        dynamic handler = ResolveHandler<IRequest>(scope, typeof(IRequestHandler<>), request.GetType());

        // Resolve HandleAsync at runtime without having to know about the implementation at buildtime.
        LogInformation((string)handler.GetType().Name);
        await (Task)handler.HandleAsync((dynamic)request);
    }

    public async Task<TResponse> InvokeAsync<TResponse>(IRequest request)
    {
        // A new scope must be created to resolve scoped services.
        using var scope = _serviceProvider.CreateScope();

        // Resolve closed handler type.
        dynamic handler = ResolveHandler<IRequest>(scope, typeof(IRequestHandler<,>), request.GetType(), typeof(TResponse));

        // Resolve HandleAsync at runtime without having to know about the implementation at buildtime.
        LogInformation((string)handler.GetType().Name);
        return await (Task<TResponse>)handler.HandleAsync((dynamic)request);
    }

    private dynamic ResolveHandler<IRequest>(IServiceScope scope, Type requestHandlerType, params Type[] genericTypes)
    {
        // Get the Type for IRequestHandler interface with IRequest as the generic type
        // to resolve the handler for the IRequest type.
        var handlerClosedType = requestHandlerType.MakeGenericType(genericTypes);

        // Resolve handler for IRequestHandler with IRequest as the generic type.
        return scope.ServiceProvider.GetRequiredService(handlerClosedType);
    }

    private void LogInformation(string handler) =>
        _logger.LogInformation("{handler}: Handling request.", handler);
}