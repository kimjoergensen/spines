namespace Spines.Shared.Exceptions;
using System;

public class MediatorException : Exception
{
    public Type Handler { get; private set; }

    public MediatorException(Type handler, Exception ex)
        : base($"An unhandled exception was thrown from {handler.Name}. See inner exception for details.", ex)
    {
        Handler = handler;
    }
}
