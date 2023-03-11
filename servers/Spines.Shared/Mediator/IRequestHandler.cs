namespace Spines.Shared.Mediator;

public interface IRequestHandler<TRequest> where TRequest : IRequest
{
    Task HandleAsync(TRequest request);
}

public interface IRequestHandler<TRequest, TResponse> where TRequest : IRequest
{
    Task<TResponse> HandleAsync(TRequest request);
}