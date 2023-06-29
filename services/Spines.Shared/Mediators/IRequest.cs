namespace Spines.Shared.Mediators;

/// <summary>
/// Inherit to define the object as a request that can be used as the parameter for an <see cref="IRequestHandler{TRequest}"/>
/// when invoked by <see cref="IMediator.InvokeAsync(IRequest)"/>.
/// </summary>
public abstract record IRequest
{
}
