namespace Identity.Core.Exceptions;
using System;

using Microsoft.AspNetCore.Identity;

public class UserRegistrationException : Exception
{
    public string Email { get; }
    public IEnumerable<IdentityError> Errors { get; }

    public UserRegistrationException(string email, IEnumerable<IdentityError> errors) : base($"Unable to register new user '{email}'.")
    {
        Email = email;
        Errors = errors;
    }
}
