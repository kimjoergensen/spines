namespace Spines.Shared.Mediators.Implementations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Spines.Shared.Exceptions;
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
        dynamic handler = Mediator.ResolveHandler<IRequest>(scope, typeof(IRequestHandler<>), request.GetType());

        await (Task)HandleRequest(handler, request);
    }

    public async Task<TResponse?> InvokeAsync<TResponse>(IRequest request)
    {
        // A new scope must be created to resolve scoped services.
        using var scope = _serviceProvider.CreateScope();

        // Resolve closed handler type.
        dynamic handler = Mediator.ResolveHandler<IRequest>(scope, typeof(IRequestHandler<,>), request.GetType(), typeof(TResponse));

        return await (Task<TResponse?>)HandleRequest(handler, request);
    }

    private static dynamic ResolveHandler<IRequest>(IServiceScope scope, Type requestHandlerType, params Type[] genericTypes)
    {
        // Get the Type for IRequestHandler interface with IRequest as the generic type
        // to resolve the handler for the IRequest type.
        var handlerClosedType = requestHandlerType.MakeGenericType(genericTypes);

        // Resolve handler for IRequestHandler with IRequest as the generic type.
        return scope.ServiceProvider.GetRequiredService(handlerClosedType);
    }

    private dynamic HandleRequest(dynamic handler, IRequest request)
    {
        var handlerType = (Type)handler.GetType();

        try
        {
            _logger.LogInformation("{handler}: Handling request.", handlerType.Name);
            return handler.HandleAsync((dynamic)request);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{handler}: An unhandled exception was thrown.", handlerType.Name);
            throw new MediatorException(handlerType, ex);
        }
        finally
        {
            _logger.LogInformation("{handler}: Finished request.", handlerType.Name);
        }
    }

}