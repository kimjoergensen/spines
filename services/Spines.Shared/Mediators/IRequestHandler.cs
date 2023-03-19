namespace Spines.Shared.Mediators;

/// <summary>
/// Handles a <see cref="IMediator"/>- <see cref="IRequest"/>.
/// When the mediator invokes the <see cref="IRequest"/> set as the generic <typeparamref name="TRequest"/>,
/// the <see cref="HandleAsync(TRequest)"/> method will be automatically invoked.
/// </summary>
/// <typeparam name="TRequest">The <see cref="IRequest"/> that is handled by the inheriting handler.</typeparam>
public interface IRequestHandler<TRequest> where TRequest : IRequest
{
    /// <summary>
    /// Automatically invoked when <see cref="IMediator.InvokeAsync(IRequest)"/>,
    /// where <see cref="IRequest"/> matches <typeparamref name="TRequest"/>, is executed.
    /// </summary>
    /// <param name="request">The request values passed through <see cref="IMediator.InvokeAsync(IRequest)"/>.</param>
    ValueTask HandleAsync(TRequest request);
}

/// <inheritdoc cref="IRequestHandler{TRequest}"/>
/// <typeparam name="TResponse">The response that is returned from the inheriting handler.</typeparam>
public interface IRequestHandler<TRequest, TResponse> where TRequest : IRequest
{
    /// <summary>
    /// Automatically invoked when <see cref="IMediator.InvokeAsync{TResponse}(IRequest)"/>,
    /// where <see cref="IRequest"/> matches <typeparamref name="TRequest"/>, is executed.
    /// </summary>
    /// <param name="request"></param>
    /// <returns><typeparamref name="TResponse"/></returns>
    ValueTask<TResponse> HandleAsync(TRequest request);
}