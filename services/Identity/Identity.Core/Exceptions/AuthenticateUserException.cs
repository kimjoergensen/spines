namespace Identity.Core.Exceptions;
using System;

public class AuthenticateUserException : Exception
{
    public string Username { get; }

    public AuthenticateUserException(string username) : base($"Unable to authenticate user '{username}'.")
    {
        Username = username;
    }
}
