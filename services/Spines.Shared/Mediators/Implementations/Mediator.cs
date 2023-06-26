namespace Spines.Shared.Mediators.Implementations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Spines.Shared.Mediators;

/// <summary>
/// <para>Mediator pattern implementation. Used to invoke <see cref="IRequest"/>,
/// which will be automatically handled by registered <see cref="IRequestHandler{TRequest}"/> or <see cref="IRequestHandler{TRequest, TResponse}"/></para>
/// 
/// <para>Invoke in Program or Startup <see cref="MediatorMiddleware.AddMediator(IServiceCollection, Assembly)"/>
/// to register mediator and request handlers.</para>
/// </summary>
internal class Mediator : IMediator
{
    private readonly ILogger _logger;
    private readonly IServiceProvider _serviceProvider;

    public Mediator(ILogger<Mediator> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    /// <summary>
    /// Invoke the registered <see cref="IRequestHandler{TRequest}"/>.
    /// </summary>
    /// <param name="request">Values to pass to <see cref="IRequestHandler{TRequest}.HandleAsync(TRequest)"/>.</param>
    public async ValueTask InvokeAsync(IRequest request)
    {
        // A new scope must be created to resolve scoped services.
        using var scope = _serviceProvider.CreateScope();

        // Resolve closed handler type.
        dynamic handler = Mediator.ResolveHandler<IRequest>(scope, typeof(IRequestHandler<>), request.GetType());

        await HandleRequest(handler, request);
    }

    /// <summary>
    /// Invoke the registered <see cref="IRequestHandler{TRequest, TResponse}"/>.
    /// </summary>
    /// <typeparam name="TResponse">Response type from the handler.</typeparam>
    /// <param name="request">Values to pass to <see cref="IRequestHandler{TRequest, TResponse}.HandleAsync(TRequest)"/>.</param>
    public async ValueTask<TResponse> InvokeAsync<TResponse>(IRequest request)
    {
        // A new scope must be created to resolve scoped services.
        using var scope = _serviceProvider.CreateScope();

        // Resolve closed handler type.
        dynamic handler = Mediator.ResolveHandler<IRequest>(scope, typeof(IRequestHandler<,>), request.GetType(), typeof(TResponse));

        return await HandleRequest(handler, request);
    }

    private static dynamic ResolveHandler<IRequest>(IServiceScope scope, Type requestHandlerType, params Type[] genericTypes)
    {
        // Get the Type for IRequestHandler interface with IRequest as the generic type
        // to resolve the handler for the IRequest type.
        var handlerClosedType = requestHandlerType.MakeGenericType(genericTypes);

        // Resolve handler for IRequestHandler with IRequest as the generic type.
        return scope.ServiceProvider.GetRequiredService(handlerClosedType);
    }

    private async ValueTask<dynamic> HandleRequest(dynamic handler, IRequest request)
    {
        var handlerType = (Type)handler.GetType();

        try
        {
            _logger.LogInformation("{handler}: Handling request.", handlerType.Name);
            return await handler.HandleAsync((dynamic)request);
        }
        finally
        {
            _logger.LogInformation("{handler}: Finished request.", handlerType.Name);
        }
    }
}