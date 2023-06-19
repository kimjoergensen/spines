namespace Spines.Shared.Exceptions;

using System;

public class NotFoundException<T> : Exception
{
    public object Identifier { get; private set; }

    public NotFoundException(object identifier) : base($"{typeof(T).Name} with identifier '{identifier}' could not be found.")
    {
        Identifier = identifier;
    }
}
