using System.Security.Claims;

namespace EnglishHelperService.API.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        /// <summary>
        /// Get user id from ClaimTypes
        /// </summary>
        public static long GetUserId(this ClaimsPrincipal user)
        {
            var nameIdentifierClaim = user?.FindFirst(ClaimTypes.NameIdentifier);

            if (nameIdentifierClaim != null && long.TryParse(nameIdentifierClaim.Value, out long userId))
            {
                return userId;
            }

            return 0;
        }

        /// <summary>
        /// Get username from ClaimTypes
        /// </summary>
        public static string GetUsername(this ClaimsPrincipal user)
        {
            return user?.FindFirst(ClaimTypes.Name)?.Value;
        }
    }
}
