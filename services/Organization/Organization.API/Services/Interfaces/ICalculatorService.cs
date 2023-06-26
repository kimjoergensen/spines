namespace Organization.API.Services.Interfaces;

using System.Runtime.Serialization;
using System.ServiceModel;

[ServiceContract]
public interface ICalculatorService
{
    ValueTask<MultiplyResult> MultiplyAsync(MultiplyRequest request);
}

[DataContract]
public class MultiplyRequest
{
    [DataMember(Order = 1)]
    public int X { get; set; }

    [DataMember(Order = 2)]
    public int Y { get; set; }
}

[DataContract]
public class MultiplyResult
{
    [DataMember(Order = 1)]
    public int Result { get; set; }
}