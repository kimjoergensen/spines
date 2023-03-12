namespace Spines.Shared.Mediators.Implementations;
using Microsoft.Extensions.DependencyInjection;

using Spines.Shared.Mediators;

internal class Mediator : IMediator
{
    private readonly IServiceProvider _serviceProvider;

    public Mediator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task InvokeAsync(IRequest request)
    {
        using var scope = _serviceProvider.CreateScope();
        var handlerType = typeof(IRequestHandler<>)
            .MakeGenericType(request.GetType());

        dynamic handler = scope.ServiceProvider.GetRequiredService(handlerType);
        await (Task)handler.HandleAsync((dynamic)request);
    }

    public async Task<TResponse> InvokeAsync<TResponse>(IRequest request)
    {
        using var scope = _serviceProvider.CreateScope();
        var handlerType = typeof(IRequestHandler<,>)
            .MakeGenericType(request.GetType(), typeof(TResponse));

        dynamic handler = scope.ServiceProvider.GetRequiredService(handlerType);
        return await (Task<TResponse>)handler.HandleAsync((dynamic)request);
    }
}