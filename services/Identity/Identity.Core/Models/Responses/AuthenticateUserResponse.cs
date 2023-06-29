namespace Identity.Core.Models.Responses;

using System;
using System.Runtime.Serialization;

[DataContract]
public class AuthenticateUserResponse
{
    [DataMember(Order = 1)]
    public required string AccessToken { get; set; }

    [DataMember(Order = 2)]
    public required TimeSpan Expires { get; set; }
}