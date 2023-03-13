namespace Identity.Core.Models.Requests;

using Spines.Shared.Mediators;

public class GetClaimsPrincipalRequest : IRequest
{
    public ApplicationUser User { get; private set; }

    public GetClaimsPrincipalRequest(ApplicationUser user)
    {
        User = user;
    }
}