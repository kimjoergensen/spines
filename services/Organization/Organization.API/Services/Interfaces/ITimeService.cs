namespace Organization.API.Services.Interfaces;

using ProtoBuf;
using ProtoBuf.Grpc;
using ProtoBuf.Grpc.Configuration;

[Service]
public interface ITimeService
{
    [Operation]
    IAsyncEnumerable<TimeResult> SubscribeAsync(CallContext context = default);
}

[ProtoContract]
public class TimeResult
{
    [ProtoMember(1)]
    public Guid Id { get; set; }

    [ProtoMember(2)]
    public DateTime Time { get; set; }
}
