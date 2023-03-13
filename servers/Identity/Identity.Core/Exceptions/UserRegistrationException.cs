namespace Identity.Core.Exceptions;
using System;

internal class UserRegistrationException : Exception
{
    public string Email { get; }

    public UserRegistrationException(string email) : base($"Unable to register new user {email}.")
    {
        Email = email;
    }
}
