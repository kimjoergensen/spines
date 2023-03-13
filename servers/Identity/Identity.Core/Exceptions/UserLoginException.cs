namespace Identity.Core.Exceptions;
using System;

using Identity.Core.Models;

internal class UserLoginException : Exception
{
    public ApplicationUser User { get; }

    public UserLoginException(ApplicationUser user) : base($"Incorrect login credentials for user {user.UserName}")
    {
        User = user;
    }
}
