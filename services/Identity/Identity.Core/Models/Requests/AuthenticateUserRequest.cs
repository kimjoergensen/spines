﻿namespace Identity.Core.Models.Requests;

using System.Runtime.Serialization;

[DataContract]
public record AuthenticateUserRequest
{
    [DataMember(Order = 1)]
    public required string Email { get; set; }

    [DataMember(Order = 2)]
    public required string Password { get; set; }
}
