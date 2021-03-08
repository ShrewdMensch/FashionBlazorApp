using System.Security.Claims;
using System.Collections.Generic;
using Domain;

namespace Application
{
    public interface IUserAccessor
    {
        AppUser GetCurrentUser();
        AppUser GetUserFromClaimsPrincipal(ClaimsPrincipal claimsPrincipal);
        string GetCurrentUsername();
        string GetCurrentUserId();
        IList<string> GetCurrentUserRoles();

        string GetCurrentUserFirstRole();
        string GetUserFirstRole(AppUser user);
    }
}