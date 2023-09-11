using System.Security.Claims;

namespace EnglishHelperService.API.Extensions
{
	public static class ClaimsPrincipalExtensions
	{
		/// <summary>
		/// Get user id from ClaimTypes
		/// </summary>
		public static int GetUserId(this ClaimsPrincipal user)
		{
			return int.Parse(user.FindFirst(ClaimTypes.NameIdentifier)?.Value);
		}

		/// <summary>
		/// Get username from ClaimTypes
		/// </summary>
		public static string GetUsername(this ClaimsPrincipal user)
		{
			return user.FindFirst(ClaimTypes.Name)?.Value;
		}
	}
}
