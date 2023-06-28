namespace Spines.Shared.Mediators.Implementations;

using System.Reflection;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Spines.Shared.Mediators;
using Spines.Shared.Middlewares;

/// <summary>Mediator pattern implementation.</summary>
/// <remarks>
/// Used to invoke a registered <see cref="IRequestHandler{TRequest}"/> or <see cref="IRequestHandler{TRequest, TResponse}"/>.
/// Add Mediator to your application with the <see cref="MediatorMiddleware.AddMediator(IServiceCollection, Assembly)"/> middleware function
/// to register Mediator and all derived request handler classes.
/// </remarks>
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
    /// Invoke the registered <see cref="IRequestHandler{TRequest}.HandleAsync(TRequest)"/> method.
    /// </summary>
    /// <param name="request">Values to pass to the <see cref="IRequestHandler{TRequest}.HandleAsync(TRequest)"/> method.</param>
    public async ValueTask InvokeAsync(IRequest request)
    {
        // A new scope must be created to resolve scoped services.
        using var scope = _serviceProvider.CreateScope();

        // Resolve closed handler type.
        dynamic handler = Mediator.ResolveHandler<IRequest>(scope, typeof(IRequestHandler<>), request.GetType());

        await HandleRequest(handler, request);
    }

    /// <summary>
    /// Invoke the registered <see cref="IRequestHandler{TRequest, TResponse}.HandleAsync(TRequest)"/> method.
    /// </summary>
    /// <typeparam name="TResponse">Response type from the handler.</typeparam>
    /// <param name="request">Values to pass to <see cref="IRequestHandler{TRequest, TResponse}.HandleAsync(TRequest)"/>.</param>
    /// <returns>Value of type <typeparamref name="TResponse"/>.</returns>
    public async ValueTask<TResponse?> InvokeAsync<TResponse>(IRequest request)
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