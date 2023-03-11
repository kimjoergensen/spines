namespace Spines.Shared.Mediator.Implementation;
using Microsoft.Extensions.DependencyInjection;

using Spines.Shared.Mediator;

internal class Mediator : IMediator
{
    private readonly IServiceProvider _serviceProvider;

    public Mediator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public Task InvokeAsync(IRequest request)
    {
        var handlerType = typeof(IRequestHandler<>)
            .MakeGenericType(request.GetType());

        dynamic handler = _serviceProvider.GetRequiredService(handlerType);
        return (Task)handler.HandleAsync((dynamic)request);
    }

    public async Task<TResponse> InvokeAsync<TResponse>(IRequest request)
    {
        var handlerType = typeof(IRequestHandler<,>)
            .MakeGenericType(request.GetType(), typeof(TResponse));

        dynamic handler = _serviceProvider.GetRequiredService(handlerType);
        var result = await (Task<TResponse>)handler.HandleAsync((dynamic)request);
        return result;
    }
}