namespace Spines.Shared.Mediators;

using System.Threading.Tasks;

using Spines.Shared.Mediators.Implementations;

/// <inheritdoc cref="Mediator"/>
public interface IMediator
{
    /// <inheritdoc cref="Mediator.InvokeAsync(IRequest)"/>
    ValueTask InvokeAsync(IRequest request);

    /// <inheritdoc cref="Mediator.InvokeAsync{TResponse}(IRequest)"/>
    ValueTask<TResponse?> InvokeAsync<TResponse>(IRequest request);
}
