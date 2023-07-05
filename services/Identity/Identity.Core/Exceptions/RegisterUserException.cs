namespace Identity.Core.Exceptions;
using System;

using Microsoft.AspNetCore.Identity;

public class RegisterUserException : Exception
{
    public string Email { get; }
    public IEnumerable<IdentityError> Errors { get; }

    public RegisterUserException(string email, IEnumerable<IdentityError> errors) : base($"Unable to register new user '{email}'.")
    {
        Email = email;
        Errors = errors;
    }
}
