﻿namespace Identity.API.Interceptors;

using global::Grpc.Core;
using global::Grpc.Core.Interceptors;

public class ExceptionLoggerInterceptor : Interceptor
{
    private readonly ILogger _logger;

    public ExceptionLoggerInterceptor(ILogger<ExceptionLoggerInterceptor> logger)
    {
        _logger = logger;
    }

    public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
        TRequest request,
        ServerCallContext context,
        UnaryServerMethod<TRequest, TResponse> continuation)
    {
        try
        {
            return await continuation(request, context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception was caught in gRPC {method}.", context.Method);
            return null;
        }
    }
}