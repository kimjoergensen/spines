namespace Spines.Shared.Middlewares;

using System.Reflection;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using Spines.Shared.Mediators;
using Spines.Shared.Mediators.Implementations;

/// <summary>
/// Middleware functions for <see cref="IMediator"/> pattern implementation.
/// </summary>
public static class MediatorMiddleware
{
    /// <summary>
    /// Register <see cref="IMediator"/> for dependency injection.
    /// </summary>
    /// <remarks>
    /// Also registers all derived classes of <see cref="IRequestHandler{TRequest}"/> and <see cref="IRequestHandler{TRequest, TResponse}"/>
    /// in <paramref name="assembly"/> to invoke
    /// <list type="bullet">
    /// <item><see cref="IRequestHandler{TRequest}.HandleAsync(TRequest)"/> when executing <see cref="IMediator.InvokeAsync(IRequest)"/>.</item>
    /// <item><see cref="IRequestHandler{TRequest, TResponse}"/> when executing <see cref="IMediator.InvokeAsync{TResponse}(IRequest)"/>.</item>
    /// </list>
    /// </remarks>
    /// <param name="services"></param>
    /// <param name="assembly"></param>
    public static void AddMediator(this IServiceCollection services, Assembly assembly)
    {
        services.AddSingleton<IMediator, Mediator>();
        services.RegisterHandlers(assembly, typeof(IRequestHandler<>));
        services.RegisterHandlers(assembly, typeof(IRequestHandler<,>));
    }

    private static void RegisterHandlers(this IServiceCollection services, Assembly assembly, Type compareType)
    {
        var types = assembly.GetTypesAssignableTo(compareType);
        services.RegisterTransient(types!, compareType);
    }

    private static IEnumerable<TypeInfo> GetTypesAssignableTo(this Assembly assembly, Type compareType)
    {
        return assembly.DefinedTypes
            .Where(x => x.IsClass
                && !x.IsAbstract
                && x != compareType
                && x.GetInterfaces()
                    .Any(i => i.IsGenericType
                        && i.GetGenericTypeDefinition() == compareType));
    }

    private static void RegisterTransient(this IServiceCollection services, IEnumerable<Type> types, Type compareType)
    {
        foreach (var type in types)
        {
            var closedInterface = type.GetInterface(compareType.Name);
            services.TryAddScoped(closedInterface!, type);
        }
    }
}
