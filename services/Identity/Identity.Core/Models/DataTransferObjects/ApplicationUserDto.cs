namespace Identity.Core.Models.DataTransferObjects;
using System.Runtime.Serialization;

[DataContract]
public class ApplicationUserDto
{
    [DataMember(Order = 1)]
    public required string Email { get; set; }

    [DataMember(Order = 2)]
    public required string Password { private get; set; }
}
