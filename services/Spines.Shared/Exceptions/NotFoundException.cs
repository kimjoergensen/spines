namespace Spines.Shared.Exceptions;

using System;

public class NotFoundException<T> : Exception
{
    public object Identifier { get; private set; }

    private NotFoundException(object identifier) : base($"{typeof(T).Name} with identifier '{identifier}' could not be found.")
    {
        Identifier = identifier;
    }

    public NotFoundException(Guid identifier) : this((object)identifier) { }
    public NotFoundException(string identifier) : this((object)identifier) { }
    public NotFoundException(int identifier) : this((object)identifier) { }
}
