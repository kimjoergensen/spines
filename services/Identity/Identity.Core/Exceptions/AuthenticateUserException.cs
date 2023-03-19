namespace Identity.Core.Exceptions;
using System;

using Identity.Core.Models;

public class AuthenticateUserException : Exception
{
    public ApplicationUser User { get; }

    public AuthenticateUserException(ApplicationUser user) : base($"Unable to authenticate user {user.UserName}.")
    {
        User = user;
    }
}
