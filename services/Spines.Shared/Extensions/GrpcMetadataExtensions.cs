namespace Spines.Shared.Extensions;

using global::Grpc.Core;

using Microsoft.AspNetCore.Identity;

using static global::Grpc.Core.Metadata;

public static class GrpcMetadataExtensions
{
    public static Metadata ConvertIdentityErrors(this Metadata trailers, IEnumerable<IdentityError> errors)
    {
        var entries = errors.Select(x => new Entry(x.Code, x.Description)).ToList();
        entries.ForEach(trailers.Add);
        return trailers;
    }
}
