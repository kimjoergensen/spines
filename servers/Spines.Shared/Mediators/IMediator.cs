namespace Spines.Shared.Mediators;

using System.Reflection;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;

using Spines.Shared.Mediators.Middlewares;

/// <summary>
/// <para>Mediator pattern implementation. Used to invoke commands and queries,
/// which will be automatically handled by registered <see cref="IRequestHandler{TRequest}"/> or <see cref="IRequestHandler{TRequest, TResponse}"/></para>
/// 
/// <para>Invoke <see cref="MediatorMiddleware.AddMediator(IServiceCollection, Assembly)"/> to register mediator and request handlers.</para>
/// </summary>
public interface IMediator
{
    /// <summary>
    /// Invoke the registered <see cref="IRequestHandler{TRequest}"/>.
    /// </summary>
    /// <param name="request">Values to pass to <see cref="IRequestHandler{TRequest}.HandleAsync(TRequest)"/>.</param>
    Task InvokeAsync(IRequest request);

    /// <summary>
    /// Invoke the registered <see cref="IRequestHandler{TRequest, TResponse}"/>.
    /// </summary>
    /// <typeparam name="TResponse">Response type from the handler.</typeparam>
    /// <param name="request">Values to pass to <see cref="IRequestHandler{TRequest, TResponse}.HandleAsync(TRequest)"/>.</param>
    /// <returns><typeparamref name="TResponse"/></returns>
    Task<TResponse> InvokeAsync<TResponse>(IRequest request);
}
