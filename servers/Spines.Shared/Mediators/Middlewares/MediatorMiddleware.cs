namespace Spines.Shared.Mediators.Middlewares;

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
    /// Register <see cref="IMediator"/>, and all <see cref="IRequestHandler{TRequest}"/> and <see cref="IRequestHandler{TRequest, TResponse}"/> in <paramref name="assembly"/>.
    /// </summary>
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
