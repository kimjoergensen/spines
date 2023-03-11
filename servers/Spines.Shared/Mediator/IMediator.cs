namespace Spines.Shared.Mediator;

using System.Threading.Tasks;

public interface IMediator
{
    Task InvokeAsync(IRequest request);
    Task<TResponse> InvokeAsync<TResponse>(IRequest request);
}
