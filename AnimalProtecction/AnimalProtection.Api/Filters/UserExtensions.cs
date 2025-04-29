using System.Security.Claims;
using System.Security.Principal;

namespace AnimalProtection.Api.Filters;

public static class UserExtensions
{
    public static Guid GetUserId(this IPrincipal user)
    {
        if (user?.Identity?.IsAuthenticated != true)
        {
            return Guid.Empty;
        }

        var claimsIdentity = user.Identity as ClaimsIdentity;
        var claim = claimsIdentity?.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");

        return claim != null && Guid.TryParse(claim.Value, out var userId) ? userId : Guid.Empty;
    }
}