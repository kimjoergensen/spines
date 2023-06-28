namespace Spines.Shared.Mediators;

using System.Reflection;

using Microsoft.Extensions.DependencyInjection;

using Spines.Shared.Middlewares;

/// <summary>
/// Qualifies the derived class as a handler for <see cref="IMediator.InvokeAsync(IRequest)"/>
/// </summary>
/// <remarks>
/// Request handlers can be registered to automatically handle Mediator invocations
/// with the <see cref="MediatorMiddleware.AddMediator(IServiceCollection, Assembly)"/> middleware.
/// </remarks>
/// <typeparam name="TRequest">The derived type of <see cref="IRequest"/> that is handled by this handler.</typeparam>
public interface IRequestHandler<TRequest> where TRequest : IRequest
{
    /// <summary>
    /// Invoked when <see cref="IMediator.InvokeAsync(IRequest)"/> is executed
    /// with the <see cref="IRequest"/> parameter being of type <typeparamref name="TRequest"/>.
    /// </summary>
    /// <param name="request">The request values passed through <see cref="IMediator.InvokeAsync(IRequest)"/>.</param>
    ValueTask HandleAsync(TRequest request);
}

/// <summary>
/// Qualifies the derived class as a handler for <see cref="IMediator.InvokeAsync{TResponse}(IRequest)"/>
/// </summary>
/// <remarks>
/// Request handlers can be registered to automatically handle Mediator invocations
/// with the <see cref="MediatorMiddleware.AddMediator(IServiceCollection, Assembly)"/> middleware.
/// </remarks>
/// <typeparam name="TResponse">The type that is returned from the inheriting handler.</typeparam>
public interface IRequestHandler<TRequest, TResponse> where TRequest : IRequest
{
    /// <summary>
    /// Invoked when <see cref="IMediator.InvokeAsync{TResponse}(IRequest)"/> is executed
    /// with the <see cref="IRequest"/> parameter being of type <typeparamref name="TRequest"/>
    /// and <see cref="TResponse"/> being of type <typeparamref name="TResponse"/>.
    /// </summary>
    /// <param name="request"></param>
    ValueTask<TResponse?> HandleAsync(TRequest request);
}