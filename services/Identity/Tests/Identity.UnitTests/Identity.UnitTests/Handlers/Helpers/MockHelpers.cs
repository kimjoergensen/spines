namespace Identity.UnitTests.Handlers.Helpers;
using Identity.Core.Models;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

internal static class MockHelpers
{
    public static Mock<UserManager<ApplicationUser>> UserManager =>
        new(Mock.Of<IUserStore<ApplicationUser>>(),
            /* IOptions<IdentityOptions> optionsAccessor */null,
            /* IPasswordHasher<TUser> passwordHasher */null,
            /* IEnumerable<IUserValidator<TUser>> userValidators */null,
            /* IEnumerable<IPasswordValidator<TUser>> passwordValidators */null,
            /* ILookupNormalizer keyNormalizer */null,
            /* IdentityErrorDescriber errors */null,
            /* IServiceProvider services */null,
            /* ILogger<UserManager<TUser>> logger */null);

    public static Mock<SignInManager<ApplicationUser>> SignInManager =>
        new(UserManager.Object,
            /* IHttpContextAccessor contextAccessor */Mock.Of<IHttpContextAccessor>(),
            /* IUserClaimsPrincipalFactory<TUser> claimsFactory */Mock.Of<IUserClaimsPrincipalFactory<ApplicationUser>>(),
            /* IOptions<IdentityOptions> optionsAccessor */null,
            /* ILogger<SignInManager<TUser>> logger */null,
            /* IAuthenticationSchemeProvider schemes */null);
}
