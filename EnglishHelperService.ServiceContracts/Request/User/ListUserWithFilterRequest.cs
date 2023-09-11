

namespace EnglishHelperService.ServiceContracts
{
	/// <summary>
	/// List user filtering request with paging
	/// </summary>
	public class ListUserWithFilterRequest : PagedListRequest
	{
		public string Username { get; set; }
		public string Email { get; set; }
	}
}
