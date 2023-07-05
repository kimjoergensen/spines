namespace Identity.Core.Models.Requests;
using System.Runtime.Serialization;

[DataContract]
public record SignOutUserRequest
{
    [DataMember(Order = 1)]
    public required string Email { get; init; }
}
