namespace Organization.API.Services;

using System.Collections.Generic;
using System.Runtime.CompilerServices;

using Organization.API.Services.Interfaces;

using ProtoBuf.Grpc;

public class TimeService : ITimeService
{
    private readonly ILogger<TimeService> _logger;
    public TimeService(ILogger<TimeService> logger)
    {
        _logger = logger;
    }

    public IAsyncEnumerable<TimeResult> SubscribeAsync(CallContext context = default)
            => SubscribeAsyncImpl(context.CancellationToken);

    private static async IAsyncEnumerable<TimeResult> SubscribeAsyncImpl([EnumeratorCancellation] CancellationToken cancel)
    {
        while (!cancel.IsCancellationRequested)
        {
            try
            {
                await Task.Delay(TimeSpan.FromSeconds(10), cancel);
            }
            catch (OperationCanceledException)
            {
                break;
            }
            yield return new TimeResult { Id = Guid.NewGuid(), Time = DateTime.UtcNow };
        }
    }
}
