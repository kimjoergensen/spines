﻿namespace Identity.Core.Services.Interfaces;

using System.Threading.Tasks;

using Identity.Core.Models.Commands;
using Identity.Core.Models.Queries;

public interface IUserService
{

    /// <inheritdoc cref="UserService.RegisterUserAsync(RegisterUserCommand)"/>
    Task RegisterUserAsync(RegisterUserCommand command);

    /// <inheritdoc cref="UserService.LogInUserAsync(LogInUserQuery)"/>
    Task LogInUserAsync(LogInUserQuery query);
}
