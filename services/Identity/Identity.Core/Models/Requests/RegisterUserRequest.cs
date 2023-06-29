namespace Identity.Core.Models.Requests;

using System.Runtime.Serialization;

[DataContract]
public record RegisterUserRequest
{
    [DataMember(Order = 1)]
    public required string Email { get; init; }

    [DataMember(Order = 2)]
    public required string Password { get; init; }
}
